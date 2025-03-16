using LibraryManagement.Data.Models;

namespace LibraryManagement.Services.Interfaces;

public interface IBookService
{
    public ICollection<Book> GetAllBooks();
    public void Create(string title, string author, int genreId, string isbn);
}