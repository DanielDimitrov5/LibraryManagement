using LibraryManagement.Data;
using LibraryManagement.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Controllers;

public class BookController : Controller
{
    private readonly ILogger<HomeController> _logger;
    
    private readonly LibraryDbContext _context;

    public BookController(ILogger<HomeController> logger, LibraryDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult All()
    {
        List<Book> books = _context.Books.Include(x=> x.Genre).ToList();
        
        return View(books);
    }
}