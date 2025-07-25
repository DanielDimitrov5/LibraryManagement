using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Data.Models;

[Index(nameof(Isbn), IsUnique = true)]
public class Book
{
    public Book()
    {
        Borrowers = new List<User>();
    }
    
    [Key]
    public int Id { get; set; }
    
    [MaxLength(256)]
    public string Title { get; set; }
    
    [MaxLength(256)]
    public string Author { get; set; }
    
    public int GenreId { get; set; }
    
    public Genre Genre { get; set; }
    
    public int Copies { get; set; }
    
    public int AvailableCopies { get; set; }

    public ICollection<User> Borrowers { get; set; }
    
    [RegularExpression(@"^\d{1,4}-\d{1,4}$", ErrorMessage = "Invalid ISBN")]
    public string Isbn { get; set; }
}