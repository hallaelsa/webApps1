using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Oblig1theAteam.Business.Orders;
using Oblig1theAteam.Business.Users;
using Oblig1theAteam.Business.Users.Models;
using Oblig1theAteam.ViewModels.User;
using System;

namespace Oblig1theAteam.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService userService;
        private readonly OrderService orderService;
        const string SessionLoggedIn = "_LoggedIn";

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
                orderService.CheckCartForOwnedItems(username, HttpContext);
                HttpContext.Session.SetString(SessionLoggedIn, username);
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
                HttpContext.Session.SetString(SessionLoggedIn, username);
            }
            else
            {
                return View("LoginFailed");
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

        public bool CheckUserExists(string email)
        {
            return userService.UserExists(email);
        }

    }




}
