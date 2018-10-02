using Oblig1theAteam.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oblig1theAteam.Business.Movies.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public int AgeRestriction { get; set; }
        public int Time { get; set; }
        public string Description { get; set; }
        public string TrailerLink { get; set; }
        public string PosterName { get; set; }
        public int Price { get; set; }
        public bool Owned { get; set; }
        public List<Genre> Genre { get; set; }
    }
}
