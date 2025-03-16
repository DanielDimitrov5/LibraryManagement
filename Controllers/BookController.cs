using LibraryManagement.Data.Models;
using LibraryManagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers;

public class BookController : Controller
{
    private readonly ILogger<HomeController> _logger;
    
    private readonly IBookService _bookService;
    
    private readonly IGenreService _genreService;

    public BookController(ILogger<HomeController> logger, IBookService bookService, IGenreService genreService)
    {
        _logger = logger;
        _bookService = bookService;
        _genreService = genreService;
    }

    [HttpGet]
    public IActionResult All()
    {
        
        // Get all books
        ICollection<Book> books = _bookService.GetAllBooks();
        
        // Log message
        _logger.Log(LogLevel.Information, "BookController::All");
        
        // Pass data to view
        return View(books);
    }

    [HttpGet]
    public IActionResult Create()
    {
        ICollection<Genre> genres = _genreService.GetAllGenres();
        
        ViewBag.Genres = genres;
        
        return View();
    }

    [HttpPost]
    public IActionResult Create(string title, string author, int genreId, string isbn)
    {
        _bookService.Create(title, author, genreId, isbn);
        
        return RedirectToAction("All");
    }
}