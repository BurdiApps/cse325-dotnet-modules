using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MvcMovie.Data;

namespace MvcMovie.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var context = new MvcMovieContext(
            serviceProvider.GetRequiredService<DbContextOptions<MvcMovieContext>>());

        if (context.Movie.Any())
        {
            return;
        }

        context.Movie.AddRange(
            new Movie
            {
                Title = "Shrek",
                ReleaseDate = DateTime.Parse("2001-5-18"),
                Genre = "Animated Comedy",
                Price = 9.99M,
                Rating = "PG"
            },
            new Movie
            {
                Title = "Hotel Transylvania",
                ReleaseDate = DateTime.Parse("2012-9-28"),
                Genre = "Animated Comedy",
                Price = 8.99M,
                Rating = "PG"
            },
            new Movie
            {
                Title = "The Addams Family",
                ReleaseDate = DateTime.Parse("1991-11-22"),
                Genre = "Comedy",
                Price = 7.99M,
                Rating = "PG"
            },
            new Movie
            {
                Title = "The Grand Budapest Hotel",
                ReleaseDate = DateTime.Parse("2014-3-28"),
                Genre = "Comedy Drama",
                Price = 10.99M,
                Rating = "R"
            }
        );

        context.SaveChanges();
    }
}