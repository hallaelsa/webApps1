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

        public IActionResult Index()
        {
            // View modellen skal ha samme navn som metoden + "ViewModel".
            var model = new IndexViewModel();

            return View(model);
        }
    }
}
