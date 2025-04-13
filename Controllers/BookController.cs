using System.Collections;
using LibraryManagement.Data.Models;
using LibraryManagement.Exceptions;
using LibraryManagement.Services.Interfaces;
using LibraryManagement.utils;
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
    public async Task<IActionResult> All()
    {
        
        // Get all books
        ICollection<Book> books = await _bookService.GetAllBooksAsync();
        
        // Log message
        _logger.Log(LogLevel.Information, "BookController::All");
        
        // Pass data to view
        return View(books);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> MyBooks()
    {
        ICollection<Book> books = await _bookService.GetMyBooksAsync();
        
        return View(books);
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create()
    {
        ICollection<Genre> genres = await _genreService.GetAllGenresAsync();
        
        ViewBag.Genres = genres;
        
        return View();
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(string title, string author, int genreId, string isbn)
    {
        await _bookService.CreateAsync(title, author, genreId, isbn);
        
        return RedirectToAction("All");
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(int id)
    {
        Book? book = await TryCatchHelper.TryCatchAsync(async () => 
            {
                Book book = await _bookService.GetBookByIdAsync(id);

                ICollection<Genre> genres = await _genreService.GetAllGenresAsync();

                ViewBag.Genres = new SelectList(genres, "Id", "Name", book.GenreId);

                return book;
            },
            _logger,
            this,
            nameof(BookController.Edit));

        return View(book);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(Book book)
    {
        await TryCatchHelper.TryCatchAsync(
            async () => await _bookService.EditAsync(book.Id, book.Title, book.Author, book.GenreId, book.Isbn),
            _logger,
            this,
            nameof(BookController.Edit)
        );
        
        return RedirectToAction("All");
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        await _bookService.DeleteAsync(id);

        return Redirect($"/");
    }
}