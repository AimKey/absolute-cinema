using System.Diagnostics;
using Absolute_cinema.Models;
using Absolute_cinema.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.IO;
using Microsoft.AspNetCore.Http;
using BusinessObjects.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Common.Constants;

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
            var user = GetCurrentUser();
            if (user == null)
                return NotFound();
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

        private User GetCurrentUser()
        {
            var userNameString = HttpContext.Session.GetString("Username");
            if (string.IsNullOrEmpty(userNameString))
            {
                return null; // or handle the case where the user is not logged in
            }
            var user = _userService.GetUserByUserName(userNameString);
            return user;
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile(UpdateProfileVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = GetCurrentUser();
            if (user == null)
                return NotFound();
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

            // Sign in again with new info
            user = GetCurrentUser();
            await RefreshUserData(user);

            return RedirectToAction("UpdateProfile");
        }
        public async Task RefreshUserData(User user)
        {
            // Sign out first
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Re-fetch the user to ensure we have the latest data
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, string.IsNullOrEmpty(user.UserDetail.FullName) ? user.Username : user.UserDetail.FullName),
                new Claim(ClaimTypes.Role, user.Role ?? RoleConstants.User),
                new Claim("UserId", user.Id.ToString()),
                new Claim("Avatar", user.UserDetail.AvatarURL ?? UserConstants.DEFAULT_AVA)
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
