using Oblig1theAteam.DBModels;
using Oblig1theAteam.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using Oblig1theAteam.Business.Users;
using Microsoft.AspNetCore.Http;
using Oblig1theAteam.Business.Movies;

namespace Oblig1theAteam.Business.Orders
{
    public class OrderService
    {
        private readonly DbService dbService;
        private readonly UserService userService;
        private readonly MovieService movieService;

        public OrderService(DbService dbService, UserService userService, MovieService movieService)
        {
            this.dbService = dbService;
            this.userService = userService;
            this.movieService = movieService;
            
        }

        public List<Models.Order> GetOrders(string email)
        {
            var orders = dbService.Orders
                .Where(o => o.User.Email == email)
                .OrderByDescending(o => o.OrderDate)
                .Select(o => ToOrder(o))
                .ToList();

            foreach(var order in orders)
            {
                order.Movies = GetMovies(order.Id);
            }
            return orders;
        }

        public List<Movies.Models.Movie> GetMovies(int orderId)
        {
            return dbService.OrderItem
            .Where(oi => oi.Order.Id == orderId)
            .Select(oi => ToMovie(oi.Movie))
            .ToList();
        }

        internal void CheckCartForOwnedItems(string email, HttpContext httpContext, int age)
        {
            var moviesInCart = httpContext.Session.GetFromJson<List<Int32>>("_MoviesInCart");

            if (moviesInCart == null || moviesInCart.Count == 0)
                return;

            var ownedMovies = GetOwnedMovies(email);
            var updatedList = moviesInCart.Where(m => ownedMovies.All(om => om.Id != m)).ToList();
            var updatedList2 = new List<Int32>();

            foreach(var movieId in updatedList)
            {
                var movie = movieService.GetMovieById(movieId);
                if(movie.AgeRestriction <= age)
                {
                    updatedList2.Add(movieId);
                }
            }

            if(updatedList.Count != updatedList2.Count)
            {
                httpContext.Session.SetString("_UnderAge", "TRUE");
            }

            httpContext.Session.SaveAsJson("_MoviesInCart", updatedList2);
            httpContext.Session.SetInt32("_CountShoppingCart", updatedList2.Count);
        }

        public List<Movies.Models.Movie> GetOwnedMovies(string email)
        {
            var orders = GetOrders(email);
            var movies = new List<Movies.Models.Movie>();

            foreach(var order in orders)
            {
                movies.AddRange(order.Movies);
            }

            return movies;
        }

        private Movies.Models.Movie ToMovie(Movie dbMovie)
        {
            return new Movies.Models.Movie
            {
                Id = dbMovie.Id,
                Title = dbMovie.Title,
                Year = dbMovie.Year,
                AgeRestriction = dbMovie.AgeRestriction,
                Time = dbMovie.Time,
                Description = dbMovie.Description,
                TrailerLink = dbMovie.TrailerLink,
                PosterName = dbMovie.PosterName
            };
        }
        
        public bool CreateOrder(string user, List<Int32> moviesInCart)
        {
            List<OrderItem> orderItems = new List<OrderItem>();

            var order = new DBModels.Order()
            {
                OrderDate = DateTime.Now,
                User = userService.GetDbUser(user)
            };
            dbService.Add(order);
            var first = dbService.SaveChanges();
            var id = order.Id;

            foreach(int movieId in moviesInCart)
            {
                orderItems.Add(new OrderItem
                {
                    Order = order,
                    Movie = dbService.Movie.Find(movieId)
                });
            }
            order.OrderItem = orderItems;
            return dbService.SaveChanges() == 0 ? false : true;
        }

        private Models.Order ToOrder(DBModels.Order dbOrder)
        {
            return new Models.Order
            {
                Id = dbOrder.Id,
                Date = dbOrder.OrderDate
            };
        }
    }
}
