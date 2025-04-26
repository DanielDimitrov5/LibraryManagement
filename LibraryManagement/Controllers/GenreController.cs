using LibraryManagement.Data.Models;
using LibraryManagement.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LibraryManagement.utils;

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
    public async Task<IActionResult> All()
    {
        // Get all genres
        ICollection<Genre> genres = await _genreService.GetAllGenresAsync();
        
        // Log message
        _logger.Log(LogLevel.Information, "GenreController::All");
        
        // Pass data to view
        return View(genres);
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create()
    {
        return View();
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(string name)
    {
        await _genreService.AddGenreAsync(name);
        
        return RedirectToAction("All");
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(int id)
    {
        Genre? genre = await TryCatchHelper.TryCatchAsync(
            async () => await _genreService.GetGenreByIdAsync(id),
            _logger,
            this,
            nameof(Edit)
        );
        
        return View(genre);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(int id, string name)
    {
       await TryCatchHelper.TryCatchAsync(
            async () => await _genreService.GetGenreByIdAsync(id),
            _logger,
            this,
            nameof(Edit)
        );

        return RedirectToAction("All");
    }
}