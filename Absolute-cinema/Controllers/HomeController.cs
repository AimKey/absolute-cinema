using System.Diagnostics;
using Absolute_cinema.Models;
using Absolute_cinema.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult UpdateProfile()
        {
            var user = _userService.GetAll().FirstOrDefault();
            if (user == null) return NotFound();
            var vm = new UpdateProfileVM
            {
                FullName = user.UserDetail.FullName,
                Gender = user.UserDetail.Gender,
                Dob = user.UserDetail.Dob,
                Phone = user.UserDetail.Phone,
                AvatarURL = user.UserDetail.AvatarURL
            };
            return View(vm);
        }

        [HttpPost]
        public IActionResult UpdateProfile(UpdateProfileVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = _userService.GetAll().FirstOrDefault(); 
            if (user == null) return NotFound();
            var userDetail = user.UserDetail;
            
            if (model.Avatar != null && model.Avatar.Length > 0)
            {
                var fileName = $"avatar_{user.Id}_{DateTime.Now.Ticks}{Path.GetExtension(model.Avatar.FileName)}";
                var webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "avatars");
                Directory.CreateDirectory(webRootPath);
                var filePath = Path.Combine(webRootPath, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    model.Avatar.CopyTo(stream);
                }
                userDetail.AvatarURL = $"/avatars/{fileName}";
            }
            
            userDetail.FullName = model.FullName;
            userDetail.Gender = model.Gender;
            userDetail.Dob = model.Dob;
            userDetail.Phone = model.Phone;
            _userService.Update(userDetail);
            return RedirectToAction("UpdateProfile");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
