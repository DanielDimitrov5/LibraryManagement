using LibraryManagement.Data.Models;

namespace LibraryManagement.Services.Interfaces;

public interface IGenreService
{
    public Task<ICollection<Genre>> GetAllGenresAsync();

    public Task<Genre> GetGenreByIdAsync(int id);

    public Task AddGenreAsync(string name);

    public Task EditAsync(int id, string name);
}