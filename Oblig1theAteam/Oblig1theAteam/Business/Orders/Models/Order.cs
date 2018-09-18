using Oblig1theAteam.DBModels;
using System;
using System.Collections.Generic;

namespace Oblig1theAteam.Business.Orders.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        public User User { get; set; }

        public List<Movie> Movies { get; set; }
    }
}
