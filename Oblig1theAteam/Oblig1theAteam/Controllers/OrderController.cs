using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Oblig1theAteam.Business.Orders;
using Oblig1theAteam.Business.Users.Models;
using Oblig1theAteam.ViewModels.Order;
using System;
using System.Collections.Generic;
using Oblig1theAteam.Extensions;
using Oblig1theAteam.Business.Movies.Models;
using Oblig1theAteam.Business.Movies;
using Oblig1theAteam.Business.Users;

namespace Oblig1theAteam.Controllers
{
    public class OrderController : Controller
    {
        private readonly OrderService orderService;
        private readonly MovieService movieService;
        private readonly UserService userService;

        const string SessionLoggedIn = "_LoggedIn";
        const string SessionMoviesInCart = "_MoviesInCart";
        const string SessionCountShoppingCart = "_CountShoppingCart";

        public OrderController(OrderService orderService, MovieService movieService, UserService userService)
        {
            this.orderService = orderService;
            this.movieService = movieService;
            this.userService = userService;
        }
        
        public IActionResult MyOrders()
        {
            var model = new MyOrdersViewModel();
            var user = HttpContext.Session.GetString(SessionLoggedIn);
            model.Orders = orderService.GetOrders(user);

            return View(model);
        }

        public IActionResult ShoppingCart()
        {
            ShoppingCartViewModel viewModel = new ShoppingCartViewModel();
            List<Int32> moviesInCart;
            moviesInCart = HttpContext.Session.GetFromJson<List<Int32>>(SessionMoviesInCart);
            if (moviesInCart == null)
            {
                moviesInCart = new List<Int32>();
            }
            viewModel.Movies = GetMoviesFromIds(moviesInCart);
            viewModel.TotalSum = GetTotalSum(viewModel.Movies);
            return View(viewModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult ShoppingCart(ShoppingCartViewModel shoppingCartViewModel)
        {
            if (ModelState.IsValid)
            {
                var userId = HttpContext.Session.GetString(SessionLoggedIn);
                var moviesInCart = HttpContext.Session.GetFromJson<List<Int32>>(SessionMoviesInCart);

                if (moviesInCart == null || moviesInCart.Count < 1)
                {
                    return View();
                }
                else
                {
                    orderService.CreateOrder(userId, moviesInCart);
                    HttpContext.Session.Remove(SessionMoviesInCart);
                    HttpContext.Session.SetInt32(SessionCountShoppingCart, 0);
                    return RedirectToAction("MyOrders");
                }
            }
            ShoppingCartViewModel viewModel = new ShoppingCartViewModel();
            List<Int32> moviesInCart2;
            moviesInCart2 = HttpContext.Session.GetFromJson<List<Int32>>(SessionMoviesInCart);
            if (moviesInCart2 == null)
            {
                moviesInCart2 = new List<Int32>();
            }
            viewModel.Movies = GetMoviesFromIds(moviesInCart2);
            viewModel.TotalSum = GetTotalSum(viewModel.Movies);
            return View(viewModel);
        }

        private List<Movie> GetMoviesFromIds(List<Int32> movieIds)
        {
            List<Movie> movies = new List<Movie>();
            foreach(int movieId in movieIds){
                var movie = movieService.GetMovieById(movieId);
                if (movie != null) movies.Add(movie);
            }
            return movies;
        }

        private int GetTotalSum(List<Movie> movies)
        {
            int total = 0;
            foreach(var movie in movies)
            {
                total += movie.Price;
            }
            return total;
        }

        public IActionResult RemoveFromCart(int movieId)
        {
            List<Int32> moviesInCart = HttpContext.Session.GetFromJson<List<Int32>>(SessionMoviesInCart);
            moviesInCart.Remove(movieId);
            HttpContext.Session.SaveAsJson(SessionMoviesInCart, moviesInCart);
            HttpContext.Session.SetInt32(SessionCountShoppingCart, moviesInCart.Count);

            return RedirectToAction("ShoppingCart");
        }
    }
}
