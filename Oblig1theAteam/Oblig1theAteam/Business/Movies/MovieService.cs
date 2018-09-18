using Oblig1theAteam.Business.Movies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oblig1theAteam.Business.Movies
{
    public class MovieService
    {
        private readonly DBModels.DbService dbService;

        public MovieService(DBModels.DbService dbService)
        {
            this.dbService = dbService;
        }

        public List<Models.Movie> GetMovies()
        {
            var allMovies = dbService.Movie
                .Select(dbOrder => ToMovie(dbOrder))
                .ToList();

            foreach (var movie in allMovies)
            {
                movie.Genre = GetGenres(movie.Id);
            }

            return allMovies;
        }

        public List<Genre> GetGenres(int id) {
            var genres = dbService.Genre
                .Where(g => g.MovieGenre.Any(mg => mg.Movie.Id == id)
                .ToList();

            return genres;
        }

        private Models.Movie ToMovie(DBModels.Movie dbMovie)
        {
            return new Models.Movie
            {
                Id = dbMovie.Id,
                Title = dbMovie.Title,
                Year = dbMovie.Year,
                AgeRestriction = dbMovie.AgeRestriction,
                Time = dbMovie.Time,
                Description = dbMovie.Description,
                TrailerLink = dbMovie.TrailerLink,

            };
        }


    }
}
