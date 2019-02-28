using System;
using System.Linq;
using System.Threading.Tasks;
using Core22.Areas.Identity.Data;
using Core22.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Core22.Data
{
    public class DbInitializer
    {
        public static async Task Initialize(IServiceProvider serviceProvider, ILogger<DbInitializer> logger)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ApplicationDbContext>>()))
            {
                await InsertDefaultData(context, logger);
            }
        }

        private static async Task InsertDefaultData(ApplicationDbContext context, ILogger<DbInitializer> logger)
        {
            try
            {
                // Look for any movies.
                if (context.Movie.Any() && context.Genre.Any())
                {
                    return;   // DB has been seeded
                }

                var genres = new Genre[]
                {
                    new Genre
                    {
                        GenreName = "Romantic Comedy"
                    },
                    new Genre
                    {
                        GenreName = "Comedy"
                    },
                    new Genre
                    {
                        GenreName = "Western"
                    }
                };



                var movies = new Movie[]
                {
                    new Movie
                    {
                        Title = "When Harry Met Sally",
                        ReleaseDate = DateTime.Parse("1989-2-12"),
                        Genre = genres[0],
                        Price = 7.99M,
                        Rating = "R"
                    },
                    new Movie
                    {
                        Title = "Ghostbusters ",
                        ReleaseDate = DateTime.Parse("1984-3-13"),
                        Genre = genres[1],
                        Price = 8.99M,
                        Rating = "G"
                     },
                     new Movie
                     {
                        Title = "Ghostbusters 2",
                        ReleaseDate = DateTime.Parse("1986-2-23"),
                        Genre = genres[1],
                        Price = 9.99M,
                        Rating = "G"
                       },
                     new Movie
                     {
                        Title = "Rio Bravo",
                        ReleaseDate = DateTime.Parse("1959-4-15"),
                        Genre = genres[2],
                        Price = 3.99M,
                        Rating = "NA"
                    }
                };

                await context.Genre.AddRangeAsync(genres);
                await context.Movie.AddRangeAsync(movies);

                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred seeding the DB.");
            }
        }
    }
}
