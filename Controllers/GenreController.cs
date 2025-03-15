using LibraryManagement.Data;
using LibraryManagement.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers;

public class GenreController : Controller
{
    private readonly ILogger<HomeController> _logger;
    
    private readonly LibraryDbContext _context;

    public GenreController(ILogger<HomeController> logger, LibraryDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult All()
    {
        List<Genre> genres = _context.Genres.ToList();
        
        return View(genres);
    }
}