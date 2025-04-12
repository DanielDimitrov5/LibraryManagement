using LibraryManagement.Data.Models;

namespace LibraryManagement.Services.Interfaces;

public interface IBookService
{
    public ICollection<Book> GetAllBooks();

    public Task<ICollection<Book>> GetMyBooksAsync();
    
    public void Create(string title, string author, int genreId, string isbn);

    public Book GetBookById(int id);
    
    public void Edit(int id, string title, string author, int genreId, string isbn);
    
    public void Delete(int id);
}