using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Oblig1theAteam.Business.Movies;
using Oblig1theAteam.Business.Orders;
using Oblig1theAteam.Business.Users;
using Oblig1theAteam.Models;
using Oblig1theAteam.ViewModels.Home;
using Oblig1theAteam.Extensions;
using Oblig1theAteam.Business.Movies.Models;
using System.Collections.Generic;
using Newtonsoft.Json;


namespace Oblig1theAteam.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserService userService;
        private readonly OrderService orderService;
        private readonly MovieService movieService;

        const string SessionDisplayType = "_DisplayType";
        const string SessionTitle = "_Title";
        const string SessionGenre = "_Genre";
        const string SessionUserLoggedIn = "_UserLoggedIn";
        const string SessionCountShoppingCart = "_CountShoppingCart";
        const string SessionMoviesInCart = "_MoviesInCart";

        public HomeController(
            UserService userService,
            OrderService orderService,
            MovieService movieService)
        {
            this.userService = userService;
            this.orderService = orderService;
            this.movieService = movieService;
        }

        public IActionResult Index()
        {

            if (string.IsNullOrEmpty(HttpContext.Session.GetString(SessionCountShoppingCart)))
            {
                HttpContext.Session.SetInt32(SessionCountShoppingCart, 0);
            }

            HttpContext.Session.SetString(SessionDisplayType, "ALL");
            return AllMovies(0);
        }

        public IActionResult ChangePage(int skip)
        {
            var displayType = HttpContext.Session.GetString(SessionDisplayType);
            switch(displayType)
            {
                case "GENRE":
                    return MoviesByGenre(skip);
                case "TITLE":
                    return MoviesByTitle(skip);
                default:
                    return AllMovies(skip);
            }
        }

        public IActionResult AllMovies(int skip)
        {
            var age = userService.GetAge(HttpContext.Session.GetString(SessionUserLoggedIn));

            var model = new IndexViewModel();
            model.Movies = movieService.GetMovies(skip, age);
            setOwnedProperty(model.Movies);
            model.Genre = movieService.GetAllGenres();
            model.Skip = skip;

            // check if there is a next page
            model.HasNext = (movieService.GetMovies(skip + 20, age).Count > 0) ? true : false;

            return View("Index", model);
        }

        private void setOwnedProperty(List<Movie> movies)
        {
            var email = HttpContext.Session.GetString(SessionUserLoggedIn);
            var ownedMovies = orderService.GetOwnedMovies(email);
            var cartIds = GetMoviesInCart();

            foreach(var movie in movies)
            {
                if(cartIds.Contains(movie.Id))
                {
                    movie.inCart = true;
                }
                else if (!string.IsNullOrEmpty(email))
                {
                    foreach (var ownedMovie in ownedMovies)
                    {
                        if (movie.Id == ownedMovie.Id)
                        {
                            movie.Owned = true;
                        }
                    }
                }

            }
            
        }

        public IActionResult MoviesByTitle(int skip)
        {
            var age = userService.GetAge(HttpContext.Session.GetString(SessionUserLoggedIn));
            var title = HttpContext.Session.GetString(SessionTitle);
            var model = new IndexViewModel();
            model.Movies = movieService.GetMoviesByTitle(title, skip, age);
            setOwnedProperty(model.Movies);
            model.Genre = movieService.GetAllGenres();
            model.Skip = skip;

            // check if there is a next page
            model.HasNext = (movieService.GetMoviesByTitle(title, skip + 20, age).Count > 0) ? true : false;

            return View("Index", model);
        }

        [HttpPost]
        public IActionResult MoviesByTitle(string title)
        {
            if(string.IsNullOrEmpty(title))
            {
                HttpContext.Session.SetString(SessionDisplayType, "ALL");
                return AllMovies(0);
            }

            HttpContext.Session.SetString(SessionDisplayType, "TITLE");
            HttpContext.Session.SetString(SessionTitle, title);

            return MoviesByTitle(0);
        }

        [HttpPost]
        public IActionResult MoviesByGenre(string genre)
        {
            if (string.IsNullOrWhiteSpace(genre))
                return RedirectToAction("Index");
            
            HttpContext.Session.SetString(SessionDisplayType, "GENRE");
            HttpContext.Session.SetString(SessionGenre, genre);
            return MoviesByGenre(0);
        }

        public IActionResult MoviesByGenre(int skip)
        {
            var age = userService.GetAge(HttpContext.Session.GetString(SessionUserLoggedIn));
            var genre = HttpContext.Session.GetString(SessionGenre);
            var model = new IndexViewModel();
            model.Movies = movieService.GetMoviesByGenre(genre, skip, age);
            setOwnedProperty(model.Movies);
            model.Genre = movieService.GetAllGenres();
            model.Skip = skip;

            // check if there is a next page
            model.HasNext = (movieService.GetMoviesByGenre(genre, skip + 20, age).Count > 0) ? true : false;

            return View("Index", model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public bool AddToShoppingCart(int id)
        {
            List<Int32> moviesInCart;
            moviesInCart = GetMoviesInCart();

            if(!moviesInCart.Contains(id))
            {
                moviesInCart.Add(id);
                HttpContext.Session.SaveAsJson(SessionMoviesInCart, moviesInCart);
                HttpContext.Session.SetInt32(SessionCountShoppingCart, moviesInCart.Count);
                return true;
            }
            return false;
        }

        public string GetMoviesByTitleJson(string title)
        {
            var age = userService.GetAge(HttpContext.Session.GetString(SessionUserLoggedIn));
            var movies = movieService.GetMoviesByTitle(title, age);

            var list = new List<KeyValuePair<string, string>>();

            var i = 0;
            foreach (var movie in movies)
            {
                if(i > 10)
                {
                    break;
                }

                var pair = new KeyValuePair<string, string>("", movie.Title);

                list.Add(pair);
            }

            return JsonConvert.SerializeObject(list);
        }

        public List<Int32> GetMoviesInCart()
        {
            var cart = HttpContext.Session.GetFromJson<List<Int32>>(SessionMoviesInCart);
            if(cart == null)
            {
                cart = new List<Int32>();
            }
            return cart;
        }
    }
}
