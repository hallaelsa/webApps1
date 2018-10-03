using System.Collections.Generic;
using Oblig1theAteam.Business.Movies.Models;
using Oblig1theAteam.DBModels;

namespace Oblig1theAteam.ViewModels.Home
{
    public class IndexViewModel
    {
        public List<Business.Movies.Models.Movie> Movies { get; set; }
        public List<Business.Movies.Models.Genre> Genre { get; set; }
        public int Skip { get; set; }
        public bool HasNext { get; set; }
        public string GenreIsSet { get; set; }
    }
}
