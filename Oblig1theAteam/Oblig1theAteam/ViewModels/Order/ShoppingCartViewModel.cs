using Oblig1theAteam.Business.Movies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oblig1theAteam.ViewModels.Order
{
    public class ShoppingCartViewModel
    {
        public int TotalSum { get; set; }
        public List<Movie> Movies { get; set; }
        public int CardNumber { get; set; }
        public int CVV { get; set; }
        public string ExpirationMonth { get; set; } 
        public string ExpirationYear { get; set; }
    }
}
