using LibraryManagement.Data;
using LibraryManagement.Data.Models;
using LibraryManagement.Exceptions;
using LibraryManagement.Services;
using LibraryManagement.Services.Interfaces;
using LibraryManagement.Test.Data;

namespace LibraryManagement.Test;

public class BeforeAll
{
    public LibraryDbContext Context { get; }
    public IGenreService GenreService { get; }

    public BeforeAll()
    {
        Context = InMemoryDbContextFactory.Create().Result;
        GenreService = new GenreService(Context);
    }
}


public class GenreTest : IClassFixture<BeforeAll>
{
    private readonly LibraryDbContext _context;
    private readonly IGenreService _genreService;

    public GenreTest(BeforeAll fixture)
    {
        _context = fixture.Context;
        _genreService = fixture.GenreService;
    }

    [Fact]
    public async Task GetAllGenresAsync_Returns_All_Genres()
    {
        ICollection<Genre> genres = await _genreService.GetAllGenresAsync();

        Assert.NotEmpty(genres);
        Assert.Equal(genres.Count, _context.Genres.Count());
    }

    [Theory]
    [InlineData(100)]
    [InlineData(200)]
    [InlineData(300)]
    public async Task GetGenreByIdAsync_Throws_When_Genre_Does_Not_Exist(int genreId)
    {
        var res = await Assert.ThrowsAsync<GenreNotFoundException>(
            async () => await _genreService.GetGenreByIdAsync(genreId));
        
        Assert.Equal($"Genre with id {genreId} not found!", res.Message);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public async Task GetGenreByIdAsync_Returns_Genre_When_Genre_Exists(int genreId)
    {
        Genre genre = await _genreService.GetGenreByIdAsync(genreId);
        
        Genre? actual = await _context.Genres.FindAsync(genreId);
        
        Assert.Equal(genre, actual);
    }

    [Fact]
    public async Task AddGenreAsync_Adds_Genre()
    {
        string genreName = "TestGenre";
        
        await _genreService.AddGenreAsync(genreName);
        
        Genre genre = await _genreService.GetGenreByIdAsync(3);
        
        Assert.Equal(3, _context.Genres.Count());
        Assert.Equal(genreName, genre.Name);
    }

    [Fact]
    public async Task EditAsync_Updates_Genre()
    {
        string newGenreName = "NewGenre";
        int id = 1;
        
        Genre genre = await _genreService.GetGenreByIdAsync(id);
        
        string oldGenreName = genre.Name;
        
        await _genreService.EditAsync(id, newGenreName);
        
        Assert.Equal(newGenreName, genre.Name);
        Assert.NotEqual(oldGenreName, genre.Name);
    }
}