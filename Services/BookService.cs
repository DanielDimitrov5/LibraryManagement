using LibraryManagement.Data;
using LibraryManagement.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Services;

public class BookService
{
    private readonly LibraryDbContext _context;

    public BookService(LibraryDbContext context)
    {
        _context = context;
    }

    public ICollection<Book> GetAllBooks()
    {
        ICollection<Book> books = _context.Books.Include(x=> x.Genre).ToList();
        
        return books;
    }
}