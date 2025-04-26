using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Data.Models;

public class Genre
{
    [Key]
    public int Id { get; set; }
    
    [MaxLength(128)]
    public string Name { get; set; }
}