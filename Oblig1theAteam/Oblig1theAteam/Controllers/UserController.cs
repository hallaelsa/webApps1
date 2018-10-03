using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Oblig1theAteam.Business.Orders;
using Oblig1theAteam.Business.Users;
using Oblig1theAteam.Business.Users.Models;
using Oblig1theAteam.ViewModels.User;

namespace Oblig1theAteam.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService userService;
        private readonly OrderService orderService;
        const string SessionUserLoggedIn = "_UserLoggedIn";

        public UserController(UserService userService, OrderService orderService)
        {
            this.userService = userService;
            this.orderService = orderService;
        }
        
        public IActionResult Register()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Register(User newUser)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            bool registerOK = userService.CreateUser(newUser);

            if (registerOK)
            {
                return View("RegistrationOk");
            } else
            {
                return View("RegistrationFailed");
            }
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            // må ha sjekk for om feltene er fyllt ut. 
            // så sjekke med databasen om brukeren finnes.
            var user = userService.Login(username, password);
            if(user)
            {
                orderService.CheckCartForOwnedItems(username, HttpContext);
                HttpContext.Session.SetString(SessionUserLoggedIn, username);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("LoginFailed");
            }
        }

        public IActionResult LoginForPurchase(string username, string password)
        {
            var user = userService.Login(username, password);
            if (user)
            {
                orderService.CheckCartForOwnedItems(username, HttpContext);
                HttpContext.Session.SetString(SessionUserLoggedIn, username);
            }
            else
            {
                return View("LoginFailed");
            }

            return RedirectToAction("ShoppingCart", "Order");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove(SessionUserLoggedIn);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult MyProfile()
        {
            var user = HttpContext.Session.GetString(SessionUserLoggedIn);
            var model = new IndexViewModel();
            model.User = userService.GetUser(user);
            return View(model);
        }

        public bool CheckUserExists(string email)
        {
            return userService.UserExists(email);
        }
    }




}
