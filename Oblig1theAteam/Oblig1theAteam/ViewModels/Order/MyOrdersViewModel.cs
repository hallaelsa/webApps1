using System;
using System.Collections.Generic;
using System.Linq;
using Oblig1theAteam.Business.Orders.Models;
using System.Threading.Tasks;
using Oblig1theAteam.Business.Users.Models;

namespace Oblig1theAteam.ViewModels.Order
{
    public class MyOrdersViewModel
    {
        public List<Business.Orders.Models.Order> Orders { get; set; }
    }
}
