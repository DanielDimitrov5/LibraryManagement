using LibraryManagement.Data;
using LibraryManagement.Data.Models;
using LibraryManagement.Services.Interfaces;

namespace LibraryManagement.Services;

public class GenreService : IGenreService
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

    public Genre GetGenreById(int id)
    {
        Genre genre = _context.Genres.FirstOrDefault(g => g.Id == id);
        
        if (genre == null)
            throw new KeyNotFoundException($"Genre with id {id} not found");
        
        return genre;
    }

    public void AddGenre(string name)
    {
        Genre genre = new Genre
        {
            Name = name
        };
        
        _context.Genres.Add(genre);
        
        _context.SaveChanges();
    }
}