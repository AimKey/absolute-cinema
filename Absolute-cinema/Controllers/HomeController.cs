using System.Diagnostics;
using Absolute_cinema.Models;
using Absolute_cinema.Models.ViewModels;
using Common.Constants;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Interfaces;
using System.IO;
using Microsoft.AspNetCore.Http;

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

        [HttpGet]
        public IActionResult UpdateProfile()
        {
            // Lấy user hiện tại, ví dụ hardcode user đầu tiên
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
            var user = _userService.GetAll().FirstOrDefault(); // Lấy user hiện tại
            if (user == null) return NotFound();
            var userDetail = user.UserDetail;
            // Xử lý upload avatar nếu có
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
            // Cập nhật các trường còn lại
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
