using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Oblig1theAteam.Business.Users;
using Oblig1theAteam.Business.Users.Models;
using Oblig1theAteam.ViewModels.User;
using System;

namespace Oblig1theAteam.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService userService;
        const string SessionLoggedIn = "_LoggedIn";

        public UserController(UserService userService)
        {
            this.userService = userService;
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

        
        public IActionResult Register()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Register(User newUser)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }

            bool registerOK = userService.CreateUser(newUser);

            if (registerOK)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            // må ha sjekk for om feltene er fyllt ut. 
            // så sjekke med databasen om brukeren finnes.
            var user = userService.Login(username, password);
            if(user)
            {
                HttpContext.Session.SetString(SessionLoggedIn, username);
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult LoginForPurchase(string username, string password)
        {
            var user = userService.Login(username, password);
            if (user)
            {
                HttpContext.Session.SetString(SessionLoggedIn, username);
            }

            return RedirectToAction("ShoppingCart", "Order");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove(SessionLoggedIn);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult MyProfile()
        {
            var user = HttpContext.Session.GetString(SessionLoggedIn);
            var model = new IndexViewModel();
            model.User = userService.GetUser(user);
            return View(model);
        }


    }




}
