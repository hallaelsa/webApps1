using Oblig1theAteam.Business.Movies.Models;
using System.Collections.Generic;
using System.Linq;

namespace Oblig1theAteam.Business.Movies
{
    public class MovieService
    {
        private readonly DBModels.DbService dbService;

        public MovieService(DBModels.DbService dbService)
        {
            this.dbService = dbService;
        }

        public List<Movie> GetMovies()
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

        public List<Genre> GetGenres(int movieId) {
            return dbService.MovieGenre
            .Where(mg => mg.Movie.Id == movieId)
            .Select(mg => ToGenre(mg.Genre))
            .ToList();
        }

        private Movie ToMovie(DBModels.Movie dbMovie)
        {
            return new Movie
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

        private Genre ToGenre(DBModels.Genre dbMovie)
        {
            return new Genre
            {
                GenreName = dbMovie.GenreName
            };
        }


    }
}
