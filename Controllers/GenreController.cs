using LibraryManagement.Data.Models;
using LibraryManagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers;

public class GenreController : Controller
{
    private readonly ILogger<HomeController> _logger;
    
    private readonly IGenreService _genreService;
    
    public GenreController(ILogger<HomeController> logger, IGenreService genreService)
    {
        _logger = logger;
        _genreService = genreService;
    }

    [HttpGet]
    public IActionResult All()
    {
        // Get all genres
        ICollection<Genre> genres = _genreService.GetAllGenres();
        
        // Log message
        _logger.Log(LogLevel.Information, "GenreController::All");
        
        // Pass data to view
        return View(genres);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(string name)
    {
        _genreService.AddGenre(name);
        
        return RedirectToAction("All");
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {  
        Genre genre = _genreService.GetGenreById(id);
        
        return View(genre);
    }

    [HttpPost]
    public IActionResult Edit(int id, string name)
    {
        _genreService.Edit(id, name);
        
        return RedirectToAction("All");
    }
}