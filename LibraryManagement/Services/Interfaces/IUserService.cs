using LibraryManagement.Data.Models;

namespace LibraryManagement.Services.Interfaces;

public interface IUserService
{
    public Task<ICollection<User>> GetAllUsersAsync();
    
    public Task BorrowBookAsync(int bookId);
    
    public Task ReturnBookAsync(int bookId);
}