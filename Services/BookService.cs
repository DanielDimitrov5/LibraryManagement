using LibraryManagement.Data;
using LibraryManagement.Data.Models;
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

    public ICollection<Book> GetAllBooks()
    {
        ICollection<Book> books = _context.Books.Include(x=> x.Genre).ToList();
        
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
        
        ICollection<Book> books = _context.Books
            .Where(x=>x.Borrowers.Contains(user))
            .Include(x=>x.Genre)
            .ToList();
        
        return books;
    }

    public void Create(string title, string author, int genreId, string isbn)
    {
        Book book = new Book
        {
            Title = title,
            Author = author,
            Genre = _genreService.GetGenreById(genreId),
            Copies = 1,
            AvailableCopies = 1,
            Isbn = isbn
        };
        
        _context.Books.Add(book);
        
        _context.SaveChanges();
    }

    public Book GetBookById(int id)
    { 
        Book? book = _context.Books.Include(x=> x.Genre).FirstOrDefault(x=> x.Id == id);

        if (book == null)
        {
            throw new NullReferenceException("Book not found");
        }
        
        return book;
    }

    public void Edit(int id, string title, string author, int genreId, string isbn)
    {
        Book book = GetBookById(id);
        
        book.Title = title;
        book.Author = author;
        book.Genre = _genreService.GetGenreById(genreId);
        book.Isbn = isbn;
        
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        Book book = GetBookById(id);
        
        _context.Books.Remove(book);
        
        _context.SaveChanges();
    }
}