using LibraryManagement.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Data;

public class LibraryDbContext : DbContext
{
    public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
    {
    }
    
    public DbSet<Book> Books { get; set; }
    
    public DbSet<Genre> Genres { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Genre>().HasData(
            new Genre { Id = 1, Name = "Fiction" },
            new Genre { Id = 2, Name = "NonFiction" },
            new Genre { Id = 3, Name = "Mystery" },
            new Genre { Id = 4, Name = "Thriller" },
            new Genre { Id = 5, Name = "Fantasy" },
            new Genre { Id = 6, Name = "ScienceFiction" },
            new Genre { Id = 7, Name = "Romance" },
            new Genre { Id = 8, Name = "Horror" },
            new Genre { Id = 9, Name = "Historical" },
            new Genre { Id = 10, Name = "Adventure" },
            new Genre { Id = 11, Name = "Biography" },
            new Genre { Id = 12, Name = "SelfHelp" },
            new Genre { Id = 13, Name = "Poetry" },
            new Genre { Id = 14, Name = "GraphicNovel" },
            new Genre { Id = 15, Name = "Children" },
            new Genre { Id = 16, Name = "YoungAdult" },
            new Genre { Id = 17, Name = "Dystopian" },
            new Genre { Id = 18, Name = "Classic" },
            new Genre { Id = 19, Name = "Spirituality" },
            new Genre { Id = 20, Name = "Science" },
            new Genre { Id = 21, Name = "Philosophy" },
            new Genre { Id = 22, Name = "Business" },
            new Genre { Id = 23, Name = "Travel" },
            new Genre { Id = 24, Name = "Cooking" }
        );
    }
}