using System.Collections.Generic;
using Oblig1theAteam.Business.Users.Models;

namespace Oblig1theAteam.ViewModels.Home
{
    public class IndexViewModel
    {
        public User User { get; set; }
        public List<Business.Orders.Models.Order> Orders { get; set; }
    }
}
