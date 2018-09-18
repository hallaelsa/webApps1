using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oblig1theAteam.DBModels
{
    public class Genre
    {
        public int id { get; set; }
        public string GenreName { get; set; }

        public virtual IList<Movie> Movie { get; set; }

    }
}
