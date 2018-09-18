using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oblig1theAteam.DBModels
{
    public class Movie
    {
        public int id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public int AgeRestriction { get; set; }
        public int Time { get; set; }
        public string Description { get; set; }
        public string TrailerLink { get; set; }

        public virtual IList<Genre> Genre {get; set;}
        public virtual OrderItem OrderItem { get; set; }
    }
}
