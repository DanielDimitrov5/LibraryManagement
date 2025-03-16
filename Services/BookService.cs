using LibraryManagement.Data;
using LibraryManagement.Data.Models;
using LibraryManagement.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Services;

public class BookService : IBookService
{
    private readonly LibraryDbContext _context;
    private readonly IGenreService _genreService;

    public BookService(LibraryDbContext context, IGenreService genreService)
    {
        _context = context;
        _genreService = genreService;
    }

    public ICollection<Book> GetAllBooks()
    {
        ICollection<Book> books = _context.Books.Include(x=> x.Genre).ToList();
        
        return books;
    }

    public void Create(string title, string author, int genreId, string isbn)
    {
        Book book = new Book
        {
            Title = title,
            Author = author,
            Genre = _genreService.GetGenreById(genreId),
            Isbn = isbn
        };
        
        _context.Books.Add(book);
        
        _context.SaveChanges();
    }
}