using Oblig1theAteam.Business.Users.Models;
using Oblig1theAteam.Business.Orders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oblig1theAteam.ViewModels.Order
{
    public class CreateOrderViewModel
    {
        public User User { get; set; }

        public Business.Orders.Models.Order Order { get; set; }

    }
}
