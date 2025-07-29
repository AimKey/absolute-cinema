using System.Diagnostics;
using Absolute_cinema.Models;
using Absolute_cinema.Models.ViewModels;
using Common.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Interfaces;

namespace Absolute_cinema.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;

        public HomeController(ILogger<HomeController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        public IActionResult Index()
        {
            //var username = HttpContext.Session.GetString("Username");
            //var role = HttpContext.Session.GetString("Role");

            //if (string.IsNullOrEmpty(username))
            //{
            //    // N?u ch?a có session thì chuy?n v? login
            //    return RedirectToAction("Login", "Account");
            //}

            //ViewBag.Username = username;
            //ViewBag.Role = role;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
