using LibraryManagement.Data;
using LibraryManagement.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Test.Data;

public class InMemoryDbContextFactory
{
    public static async Task<LibraryDbContext> Create()
    {
        var options =  new DbContextOptionsBuilder<LibraryDbContext>()
            .UseInMemoryDatabase("LibraryTests")
            .Options;

        var context = new LibraryDbContext(options);

        await SeedData(context);

        return context;
    }

    private static async Task SeedData(LibraryDbContext context)
    {
        // Seed genres
        await context.Genres.AddRangeAsync(new Genre
            {
                Id = 1,
                Name = "Fantasy"
            },
            new Genre
            {
                Id = 2,
                Name = "Action"
            }
        );

    // Seed books
        await context.Books.AddRangeAsync(new Book
            {
                Title = "Book 1",
                Author = "Author 1",
                Isbn = "111-222",
                GenreId = 1
            },
            new Book
            {
                Title = "Book 2",
                Author = "Author 2",
                Isbn = "121-222"
            }
        );

        await context.SaveChangesAsync();
    }
}