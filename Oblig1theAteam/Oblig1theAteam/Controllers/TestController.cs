using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Oblig1theAteam.Controllers
{
    public class TestController : Controller
    {
        private readonly DBModels.DbService dbService;

        public TestController(DBModels.DbService dbService)
        {
            this.dbService = dbService;
        }

        // Her kan man teste spørringene sine!! URL/test for output av JSON
        public IActionResult Index()
        {
            var result = dbService.Movie
                .Select(m => new
                {
                    m.Id,
                    m.Title,
                    Genres = m.MovieGenre
                        .Select(mg => mg.Genre.GenreName)
                        .ToList()
                })
                .ToList();

            return Json(result);
        }

        // teste med    URL/test/test2/?genre=action 
        public IActionResult Test2(string genre)
        {
            var result = dbService.Movie
                .Where(m => m.MovieGenre.Any(mg => mg.Genre.GenreName == genre))
                .Select(m => new
                {
                    m.Id,
                    m.Title
                })
                .ToList();

            return Json(result);
        }
    }
}