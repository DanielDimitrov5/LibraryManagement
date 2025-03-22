using LibraryManagement.Data.Models;

namespace LibraryManagement.Services.Interfaces;

public interface IGenreService
{
    public ICollection<Genre> GetAllGenres();

    public Genre GetGenreById(int id);

    public void AddGenre(string name);

    public void Edit(int id, string name);
}