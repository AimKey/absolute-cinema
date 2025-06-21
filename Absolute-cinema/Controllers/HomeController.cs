using System.Diagnostics;
using Absolute_cinema.Models;
using Absolute_cinema.Models.ViewModels;
using Common.Constants;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Interfaces;

namespace Absolute_cinema.Controllers
{
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
            var firstUser = _userService.GetAll().FirstOrDefault();
            var userVM = new TestUserVM
            {
                Username = firstUser.Username,
                Role = firstUser.Role,
                Password = firstUser.Password,
                UserDetail = firstUser.UserDetail,
            };
            return View(userVM);
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
