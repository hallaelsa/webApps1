using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Oblig1theAteam.Business.Orders;
using Oblig1theAteam.Business.Users.Models;
using Oblig1theAteam.ViewModels.Order;

namespace Oblig1theAteam.Controllers
{
    public class OrderController : Controller
    {
        private readonly OrderService orderService;
        const string SessionLoggedIn = "_LoggedIn";

        public OrderController(OrderService orderService)
        {
            this.orderService = orderService;
        }
        
        public IActionResult MyOrders()
        {
            // View modellen skal ha samme navn som metoden + "ViewModel".
            var model = new MyOrdersViewModel();
            //model.User = userService.Get(1);
            //model.Orders = orderService.ListByUser(model.User.Id);
            var user = HttpContext.Session.GetString(SessionLoggedIn);
            model.Orders = orderService.GetOrders(user);
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
