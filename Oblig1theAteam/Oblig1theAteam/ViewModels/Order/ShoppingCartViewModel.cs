using Oblig1theAteam.Business.Movies.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Oblig1theAteam.ViewModels.Order
{
    public class ShoppingCartViewModel
    {
        public int TotalSum { get; set; }
        public List<Movie> Movies { get; set; }

        [Required(ErrorMessage = "Missing card number!")]
        [RegularExpression(@"[0-9]{16}", ErrorMessage = "Card number is invalid, must be 16 digits!")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "Missing CVV!")]
        [RegularExpression(@"[0-9]{3}", ErrorMessage = "CVV is invalid, must be 3 digits!")]
        public string CVV { get; set; }

        [Required(ErrorMessage = "Missing expiration month!")]
        [RegularExpression(@"1[012]|0?[1-9]", ErrorMessage = "Month must be MM e.g. 03")]
        public string ExpirationMonth { get; set; }

        [Required(ErrorMessage = "Missing expiration year!")] 
        [RegularExpression(@"[1][8-9]|[2][0-9]", ErrorMessage = "Year must be YY e.g. 19")]
        public string ExpirationYear { get; set; }
    }
}
