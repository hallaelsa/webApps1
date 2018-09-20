using Microsoft.AspNetCore.Mvc;
using Oblig1theAteam.Business.Users;
using Oblig1theAteam.ViewModels.User;

namespace Oblig1theAteam.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService userService;

        public UserController(UserController userService)
        {
            this.userService = userService;
        }

        public ActionResult Registrer()
        {
            return View();
        }   

        [HttpPost]
        public IActionResult Index()//string email)
        {
            // View modellen skal ha samme navn som metoden + "ViewModel".
            var model = new IndexViewModel();
            //model.User = userService.Get(1);
            //model.Orders = orderService.ListByUser(model.User.Id);
            model.User = userService.GetUser("fiskebolle@gmail.com");

            return View(model);
        }


    }




}
