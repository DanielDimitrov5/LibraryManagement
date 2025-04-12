using LibraryManagement.Data.Models;
using LibraryManagement.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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

    [HttpGet("/")]
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
    [Authorize(Roles = "Admin")]
    public IActionResult Create()
    {
        ICollection<Genre> genres = _genreService.GetAllGenres();
        
        ViewBag.Genres = genres;
        
        return View();
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public IActionResult Create(string title, string author, int genreId, string isbn)
    {
        _bookService.Create(title, author, genreId, isbn);
        
        return RedirectToAction("All");
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public IActionResult Edit(int id)
    {
        Book book = _bookService.GetBookById(id);
        
        ICollection<Genre> genres = _genreService.GetAllGenres();
        
        ViewBag.Genres = new SelectList(genres, "Id", "Name", book.GenreId);
        
        return View(book);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public IActionResult Edit(Book book)
    {
        _bookService.Edit(book.Id, book.Title, book.Author, book.GenreId, book.Isbn);
        
        return RedirectToAction("All");
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public IActionResult Delete(int id)
    {
        _bookService.Delete(id);

        return Redirect($"/");
    }
}