using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Oblig1theAteam.DBModels
{
    public static class DbInit
    {
        public static CultureInfo NorwegianCultureInfo = new CultureInfo("nb-NO");

        public static void Initialize(IServiceScope serviceScope)
        {
            var dbContext = serviceScope.ServiceProvider.GetRequiredService<DBModels.DbService>();
            dbContext.Database.EnsureCreated();

            if (!dbContext.Movie.Any())
            {
                seedMovies(dbContext);
                seedUsers(dbContext);
                seedOrders(dbContext);
            }
        }

        private static void seedOrders(DbService dbContext)
        {
            using (var reader = new StreamReader(@".\DBModels\SeedData\orders.csv"))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if(line != null)
                    {
                        // 0: Email, 1: Date+Time (format dd/mm/yyyy hh:mm:ss)
                        var columns = line.Split('|');

                        List<string> movies = columns[2].Split(',').ToList();
                        User user = dbContext.Users.Find(columns[1]);

                        Order order = new Order
                        {
                            User = user,
                            OrderDate = DateTime.Parse(columns[0], NorwegianCultureInfo, DateTimeStyles.NoCurrentDateDefault),
                        };

                        dbContext.Orders.Add(order);


                        foreach (var movieId in movies)
                        {
                            Movie movie = dbContext.Movie.Find(System.Convert.ToInt32(movieId));
                            OrderItem item = new OrderItem
                            {
                                Order = order,
                                Movie = movie,
                            };
                            dbContext.OrderItem.Add(item);
                        }
                    }
                    dbContext.SaveChanges();
                }
            }
        }

        private static void seedUsers(DbService dbContext)
        {
            using (var reader = new StreamReader(@".\DBModels\SeedData\users.csv"))
            {
                
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if(line != null)
                    {
                        // 0: Email, 1: FirstName, 2: LastName, 3: Birthday, 4: Password, 5: PhoneNumber
                        var columns = line.Split('|');

                        var newUser = new User
                        {
                            Email = columns[0],
                            FirstName = columns[1],
                            LastName = columns[2],
                            Birthday = DateTime.Parse(columns[3], NorwegianCultureInfo, DateTimeStyles.NoCurrentDateDefault),
                            Password = columns[4],
                            PhoneNumber = columns[5],
                        };
                        dbContext.Add(newUser);
                    }

                }
            }
            dbContext.SaveChanges();
        }

        public static int generateRandomPrice()
        {
            Random rnd = new Random();
            var random = rnd.Next(0, 4);
            var price = 0;

            switch(random)
            {
                case 0:
                    price = 79;
                    break;
                case 1:
                    price = 99;
                    break;
                case 2:
                    price = 129;
                    break;
                default:
                    price = 169;
                    break;
            }

            return price;
        }

        private static void seedMovies(DbService dbContext)
        {
            using (var reader = new StreamReader(@".\DBModels\SeedData\movies.csv"))
            {
                var count = 2000;
                while(count > 0 && !reader.EndOfStream)

                {
                    count--;
                    var line = reader.ReadLine();
                    if(line != null)
                    {
                        // 0: title, 1: year, 2: age, 3: time, 4: genre, 5: description, 6: poster
                        var columns = line.Split('|');
                        var trailerLink = "";

                        if (columns.Length > 7)
                            trailerLink = columns[7];


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
                                Price = generateRandomPrice(),
                                TrailerLink = trailerLink
                            };

                            dbContext.Add(newMovie);

                            var genreArray = columns[4].Split(',').ToList();

                            foreach (var genre in genreArray)
                            {
                                var trimmedGenre = genre.Trim();

                                dbContext.Add(new MovieGenre
                                {
                                    Movie = newMovie,
                                    Genre = new Genre { GenreName = trimmedGenre },
                                });
                            }
                        }
                        catch (Exception feil)
                        {

                        }
                    }

                }
                dbContext.SaveChanges();
            }
        }

    }
}
