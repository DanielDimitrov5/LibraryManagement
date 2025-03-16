using LibraryManagement.Data;
using LibraryManagement.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Services;

public class GenreService
{
    private readonly LibraryDbContext _context;

    public GenreService(LibraryDbContext context)
    {
        _context = context;
    }

    public ICollection<Genre> GetAllGenres()
    {
        ICollection<Genre> genres = _context.Genres.ToList();
        
        return genres;
    }
}