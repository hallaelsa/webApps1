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
            return dbService.Movie
                .Select(dbOrder => ToMovie(dbOrder))
                .ToList();
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
                TrailerLink = dbMovie.TrailerLink

            };
        }


    }
}
