<<<<<<< HEAD
﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
=======
﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
>>>>>>> 47c60fa2bc3dfdc70e854300c365f0328e7db8bd
using Oblig1theAteam.Business.Orders;
using Oblig1theAteam.Business.Users.Models;
using Oblig1theAteam.ViewModels.Order;
using System;
using System.Collections.Generic;
using Oblig1theAteam.Extensions;
using Oblig1theAteam.Business.Movies.Models;
using Oblig1theAteam.Business.Movies;

namespace Oblig1theAteam.Controllers
{
    public class OrderController : Controller
    {
        private readonly OrderService orderService;
<<<<<<< HEAD
        private readonly MovieService movieService;
=======
        const string SessionLoggedIn = "_LoggedIn";
>>>>>>> 47c60fa2bc3dfdc70e854300c365f0328e7db8bd

        public OrderController(OrderService orderService, MovieService movieService)
        {
            this.orderService = orderService;
            this.movieService = movieService;
        }
        
        public IActionResult MyOrders()
        {
            // View modellen skal ha samme navn som metoden + "ViewModel".
            var model = new MyOrdersViewModel();
            //model.User = userService.Get(1);
            //model.Orders = orderService.ListByUser(model.User.Id);
            var user = HttpContext.Session.GetString(SessionLoggedIn);
            model.Orders = orderService.GetOrders(user);
            //User user = new User();
            //user.Email = "eple@eple.no";
            //model.User = user;

            return View(model);
        }

        public IActionResult ShoppingCart()
        {
            ShoppingCartViewModel viewModel = new ShoppingCartViewModel();
            List<Int32> moviesInCart;
            moviesInCart = HttpContext.Session.GetFromJson<List<Int32>>("moviesInCart");
            if (moviesInCart == null)
            {
                moviesInCart = new List<Int32>();
            }
            viewModel.Movies = GetMoviesFromIds(moviesInCart);
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
            List<Int32> moviesInCart = HttpContext.Session.GetFromJson<List<Int32>>("moviesInCart");
            moviesInCart.Remove(movieId);
            HttpContext.Session.SaveAsJson("moviesInCart", moviesInCart);

            return RedirectToAction("ShoppingCart");
        }

        public IActionResult CompletePurchase()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateOrder(CreateOrderViewModel orderViewModel)
        {
            return View();
        }
    }
}
