using LibraryManagement.Data;
using LibraryManagement.Data.Models;
using LibraryManagement.Exceptions;
using LibraryManagement.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Services;

public class GenreService : IGenreService
{
    private readonly LibraryDbContext _context;

    public GenreService(LibraryDbContext context)
    {
        _context = context;
    }

    public async Task<ICollection<Genre>> GetAllGenresAsync()
    {
        ICollection<Genre> genres = await _context.Genres.ToListAsync();
        
        return genres;
    }

    public async Task<Genre> GetGenreByIdAsync(int id)
    {
        Genre? genre = await _context.Genres.FirstOrDefaultAsync(g => g.Id == id);

        if (genre == null)
        {
            throw new GenreNotFoundException($"Genre with id {id} not found!");
        }
        
        return genre;
    }

    public async Task AddGenreAsync(string name)
    {
        Genre genre = new Genre
        {
            Name = name
        };
        
        await _context.Genres.AddAsync(genre);
        
        await _context.SaveChangesAsync();
    }

    public async Task EditAsync(int id, string name)
    {
        Genre genre = await GetGenreByIdAsync(id);
        
        genre.Name = name;

       await _context.SaveChangesAsync();
    }
}