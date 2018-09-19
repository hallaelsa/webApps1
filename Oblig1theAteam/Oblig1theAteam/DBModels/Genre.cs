using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Oblig1theAteam.DBModels
{
    public class Genre
    {
        [Key]
        public string GenreName { get; set; }

        public virtual IList<MovieGenre> MovieGenre { get; set; }

    }
}
