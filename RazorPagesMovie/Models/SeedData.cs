using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Data;

namespace RazorPagesMovie.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var context = new RazorPagesMovieContext(
            serviceProvider.GetRequiredService<DbContextOptions<RazorPagesMovieContext>>());

        if (context.Movie.Any())
        {
            return;
        }

        context.Movie.AddRange(
            new Movie { Title = "Shrek", ReleaseDate = DateTime.Parse("2001-4-22"), Genre = "Animation", Price = 9.99M, Rating = "PG" },
            new Movie { Title = "Shrek 2", ReleaseDate = DateTime.Parse("2004-5-19"), Genre = "Animation", Price = 9.99M, Rating = "PG" },
            new Movie { Title = "Shrek the Third", ReleaseDate = DateTime.Parse("2007-5-18"), Genre = "Animation", Price = 9.99M, Rating = "PG" },
            new Movie { Title = "Shrek Forever After", ReleaseDate = DateTime.Parse("2010-5-21"), Genre = "Animation", Price = 9.99M, Rating = "PG" }
        );

        context.SaveChanges();
    }
}