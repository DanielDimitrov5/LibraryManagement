using LibraryManagement.Data;
using LibraryManagement.Data.Models;
using LibraryManagement.Exceptions;
using LibraryManagement.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Services;

public class BookService : IBookService
{
    private readonly LibraryDbContext _context;
    private readonly IGenreService _genreService;
    private readonly UserManager<User> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public BookService(LibraryDbContext context, IGenreService genreService, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor, IUserService userService)
    {
        _context = context;
        _genreService = genreService;
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ICollection<Book>> GetAllBooksAsync()
    {
        ICollection<Book> books = await _context.Books.Include(x=> x.Genre).ToListAsync();
        
        return books;
    }

    public async Task<ICollection<Book>> GetMyBooksAsync()
    {
        var userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
        User user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            return null;
        }
        
        ICollection<Book> books = await _context.Books
            .Where(x=>x.Borrowers.Contains(user))
            .Include(x=>x.Genre)
            .ToListAsync();
        
        return books;
    }

    public async Task CreateAsync(string title, string author, int genreId, string isbn)
    {
        Book book = new Book
        {
            Title = title,
            Author = author,
            Genre = await _genreService.GetGenreByIdAsync(genreId),
            Copies = 1,
            AvailableCopies = 1,
            Isbn = isbn
        };
        
        await _context.Books.AddAsync(book);
        
        await _context.SaveChangesAsync();
    }

    public async Task<Book> GetBookByIdAsync(int id)
    { 
        Book? book = await _context.Books.Include(x=> x.Genre).FirstOrDefaultAsync(x=> x.Id == id);

        if (book == null)
        {
            throw new BookNotFoundException($"Book with id {id} was not found");
        }
        
        return book;
    }

    public async Task EditAsync(int id, string title, string author, int genreId, string isbn)
    {
        Book book = await GetBookByIdAsync(id);
        
        book.Title = title;
        book.Author = author;
        book.Genre = await _genreService.GetGenreByIdAsync(genreId);
        book.Isbn = isbn;
        
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        Book book = await GetBookByIdAsync(id);
        
        _context.Books.Remove(book);
        
        await _context.SaveChangesAsync();
    }
}