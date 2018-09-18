using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oblig1theAteam.DBModels
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

        public virtual IList<MovieGenre> MovieGenre {get; set;}
        public virtual IList<OrderItem> OrderItem { get; set; }
    }
}
