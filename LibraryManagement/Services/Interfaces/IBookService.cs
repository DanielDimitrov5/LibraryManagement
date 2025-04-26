using LibraryManagement.Data.Models;

namespace LibraryManagement.Services.Interfaces;

public interface IBookService
{
    public Task<ICollection<Book>> GetAllBooksAsync();

    public Task<ICollection<Book>> GetMyBooksAsync();
    
    public Task CreateAsync(string title, string author, int genreId, string isbn);

    public Task<Book> GetBookByIdAsync(int id);
    
    public Task EditAsync(int id, string title, string author, int genreId, string isbn);
    
    public Task DeleteAsync(int id);
}