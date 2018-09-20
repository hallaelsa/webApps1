using Microsoft.AspNetCore.Mvc;
using Oblig1theAteam.Business.Orders;
using Oblig1theAteam.Business.Users.Models;
using Oblig1theAteam.ViewModels.Order;

namespace Oblig1theAteam.Controllers
{
    public class OrderController : Controller
    {
        private readonly OrderService orderService;

        public OrderController(OrderService orderService)
        {
            this.orderService = orderService;
        }
        
        public IActionResult Orders()//string email)
        {
            // View modellen skal ha samme navn som metoden + "ViewModel".
            var model = new OrdersViewModel();
            //model.User = userService.Get(1);
            //model.Orders = orderService.ListByUser(model.User.Id);
            model.Orders = orderService.GetOrders("eple@eple.no");
            //User user = new User();
            //user.Email = "eple@eple.no";
            //model.User = user;

            return View(model);
        }

        public IActionResult ShoppingCart()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateOrder(CreateOrderViewModel orderViewModel)
        {
            return View();
        }
    }
}
