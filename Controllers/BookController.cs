using LibraryManagement.Data;
using LibraryManagement.Data.Models;
using LibraryManagement.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Controllers;

public class BookController : Controller
{
    private readonly ILogger<HomeController> _logger;
    
    private readonly BookService _bookService;

    public BookController(ILogger<HomeController> logger, BookService bookService)
    {
        _logger = logger;
        _bookService = bookService;
    }

    public IActionResult All()
    {
        
        // Get all books
        ICollection<Book> books = _bookService.GetAllBooks();
        
        // Log message
        _logger.Log(LogLevel.Information, "BookController::All");
        
        // Pass data to view
        return View(books);
    }
}