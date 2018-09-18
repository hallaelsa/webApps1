using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Oblig1theAteam.DBModels
{
    public static class DbInit
    {
        public static void Initialize(IServiceScope serviceScope)
        {
            var dbContext = serviceScope.ServiceProvider.GetRequiredService<DBModels.DbService>();
            dbContext.Database.EnsureCreated();

            if (!dbContext.Movie.Any())
            {
                seedGenres(dbContext);
                seedMovies(dbContext);

            }
        }

        private static void seedGenres(DbService dbContext)
        {
            using (var reader = new StreamReader(@".\DBModels\SeedData\genres.csv"))
            {
                while (!reader.EndOfStream)
                {
                    var genre = reader.ReadLine();
                    dbContext.Add(new Genre { GenreName = genre });
                    dbContext.SaveChanges();
                }

            }
        }

        private static void seedMovies(DbService dbContext)
        {
            using (var reader = new StreamReader(@".\DBModels\SeedData\movies.csv"))
            {
                while(!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var columns = line.Split('|'); // 0: title, 1: year, 2: age, 3: time, 4: genre, 5: description, 6: poster

                    try
                    {
                        var newMovie = new Movie
                        {
                            Title = columns[0],
                            Year = Int32.Parse(columns[1]),
                            AgeRestriction = Int32.Parse(columns[2]),
                            Time = Int32.Parse(columns[3]),
                            Description = columns[5],
                            PosterName = columns[6],
                            TrailerLink = ""
                        };

                        dbContext.Add(newMovie);

                        var genreArray = columns[4].Split(',').ToList();
                        genreArray.ForEach(g => g.Trim());

                        foreach (var genre in genreArray)
                        {
                            dbContext.Add(new MovieGenre
                            {
                                Movie = newMovie,
                                Genre = new Genre { GenreName = genre },
                            });
                        }

                        dbContext.SaveChanges();
                    }
                    catch(Exception feil)
                    {

                    }
                }
            }
        }

    }
}
