﻿using Oblig1theAteam.Business.Movies.Models;
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
        
        public List<Movie> GetMoviesByGenre(string genre)
        {
            var movies = dbService.MovieGenre
                .Where(mg => mg.Genre.GenreName == genre)
                /// Take() må være FØR select!! 
                .Take(20)
                .Select(mg => ToMovie(mg.Movie))
                .ToList();

            return AddGenreToMovieModel(movies);
        }
        
        public List<Movie> GetMoviesByTitle(string title)
        {
            var movies = dbService.Movie
                .Where(m => m.Title.Contains(title))
                .Take(20)
                .Select(dbMovie => ToMovie(dbMovie))
                .ToList();            

            return AddGenreToMovieModel(movies);
        }
        
        public Movie GetMovieById(int movieId)
        {
            var movie = dbService.Movie
            .FirstOrDefault(m => m.Id == movieId);
            return ToMovie(movie);
        }

        public List<Movie> AddGenreToMovieModel(List<Movie> movies)
        {
            foreach (var movie in movies)
            {
                movie.Genre = GetGenres(movie.Id);
            }

            return movies;
        }
        
        public List<Movie> GetMovies()
        {
            var allMovies = dbService.Movie
                .Take(20)
                .Select(dbMovie => ToMovie(dbMovie))
                .ToList();

            return AddGenreToMovieModel(allMovies); ;
        }

        public List<Genre> GetGenres(int movieId) {
            return dbService.MovieGenre
            .Where(mg => mg.Movie.Id == movieId)
            .Select(mg => ToGenre(mg.Genre))
            .ToList();
        }

        public List<Genre> GetAllGenres()
        {
            return dbService.Genre
                .Select(g => new Genre
                {
                    GenreName = g.GenreName
                })
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
                PosterName = dbMovie.PosterName,
                Price = dbMovie.Price,
                TrailerLink = dbMovie.TrailerLink,
            };
        }

        private Genre ToGenre(DBModels.Genre dbGenre)
        {
            return new Genre
            {
                GenreName = dbGenre.GenreName
            };
        }
    }
}
