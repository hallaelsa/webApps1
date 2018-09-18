using Microsoft.AspNetCore.Mvc;
using Oblig1theAteam.Business.Orders;
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

        /*public ActionResult Index()
        {
            return View();
        }*/

        //[HttpPost]
        public IActionResult Index()//string email)
        {
            // View modellen skal ha samme navn som metoden + "ViewModel".
            var model = new IndexViewModel();
            //model.User = userService.Get(1);
            //model.Orders = orderService.ListByUser(model.User.Id);
            model.Orders = orderService.GetOrders("eple@eple.no");

            return View(model);
        }
    }
}
