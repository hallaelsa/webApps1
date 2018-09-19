using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Oblig1theAteam.Business.Movies;
using Oblig1theAteam.Business.Orders;
using Oblig1theAteam.Business.Users;
using Oblig1theAteam.DependencyInjectionDemo;
using Oblig1theAteam.Models;
using Oblig1theAteam.ViewModels.Home;

namespace Oblig1theAteam.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserService userService;
        private readonly OrderService orderService;
        private readonly MovieService movieService;

        public HomeController(
            UserService userService,
            OrderService orderService,
            MovieService movieService)
        {
            // Vi henter inn userService for å jobbe med brukere. Vi skal aldri bruke dbModels direkte. 
            // det er bare service som vet om databasen
            // viewModel skal bruke User fra service, og ikke fra dbModel!
            this.userService = userService;
            this.orderService = orderService;
            this.movieService = movieService;
        }

        public IActionResult Index()
        {
            // Her bruker vi businesslogikken (altså servicemodellene) til å fikse ViewModel
            var model = new IndexViewModel();
            model.Movies = movieService.GetMovies();
            model.Genre = movieService.GetAllGenres();

            return View(model);
        }

        [HttpPost]
        public IActionResult MoviesByTitle(string title)
        {
            var model = new IndexViewModel();
            model.Movies = movieService.GetMoviesByTitle(title);
            model.Genre = movieService.GetAllGenres();

            return View("Index",model);
        }

        [HttpPost]
        public IActionResult MoviesByGenre(string genre)
        {
            var model = new IndexViewModel();
            model.Movies = movieService.GetMoviesByGenre(genre);
            model.Genre = movieService.GetAllGenres();

            return View("Index", model);
        }

        //public IActionResult Demo()
        //{
        //    return new JsonResult(demoService.Add());
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
