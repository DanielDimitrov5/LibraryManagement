using LibraryManagement.Data;
using LibraryManagement.Data.Models;
using LibraryManagement.Exceptions;
using LibraryManagement.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Services;

public class UserService : IUserService
{
    private readonly LibraryDbContext _context;
    private readonly UserManager<User> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(LibraryDbContext context, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task<ICollection<User>> GetAllUsersAsync()
    {
        ICollection<User> users = await _context.Users.ToListAsync();
        
        return users;
    }

    public async Task BorrowBookAsync(int bookId)
    {
        Book? book = await _context.Books.FirstOrDefaultAsync(x=>x.Id == bookId);
        
        if (book == null)
        {
            throw new BookNotFoundException($"Book with id {bookId} not found!");
        }

        if (book.AvailableCopies < 1)
        {
            throw new NoAvailableCopiesException("No available copies!");
        }
        
        string userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
        User user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            throw new UserNotFoundException("User not found");
        }
        
        book.AvailableCopies--;
        
        user.BorrowedBooks.Add(book);
        
        await _context.SaveChangesAsync();
    }

    public async Task ReturnBookAsync(int bookId)
    {
        var userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
        
        User? user = await _context.Users
            .Include(x=>x.BorrowedBooks)
            .FirstOrDefaultAsync(x => x.Id == userId);
        
        if (user == null)
        {
            throw new UserNotFoundException("User not found");
        }
        
        Book? book = user.BorrowedBooks.FirstOrDefault(x=> x.Id == bookId);

        if (book == null)
        {
            throw new BookNotFoundException($"Book with id {bookId} not found!");
        }
        
        user.BorrowedBooks.Remove(book);
        
        book.AvailableCopies++;
        
        await _context.SaveChangesAsync();
    }
}