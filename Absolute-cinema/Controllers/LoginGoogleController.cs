using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using Services.Interfaces;
using Common.Constants;

namespace Absolute_cinema.Controllers
{
    public class LoginGoogleController : Controller
    {
        private readonly IUserService _userService;

        public LoginGoogleController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GoogleLogin()
        {
            var redirectUrl = Url.Action("GoogleResponse", "LoginGoogle");
            var properties = new AuthenticationProperties
            {
                RedirectUri = redirectUrl
            };

            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [HttpGet]
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var claims = result.Principal?.Identities.FirstOrDefault()?.Claims;

            var email = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var name = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

            if (email == null)
            {
                TempData["ErrorMessage"] = "Google login failed.";
                return RedirectToAction("Login", "Account");
            }

            //  Kiểm tra xem user đã tồn tại chưa
            var user = _userService.GetUserByEmail(email);
            bool accountExist = user != null;
            if (user == null)
            {
                // Nếu chưa có, tạo user mới
                await _userService.RegisterUserAsync(
                    username: email.Split('@')[0],
                    email: email,
                    password: "Google_OAuth", // bạn có thể dùng placeholder
                    fullName: name ?? email,
                    phone: null,
                    gender: null,
                    dateOfBirth: null,
                    baseUrl: $"{Request.Scheme}://{Request.Host}"
                );
                user = _userService.GetUserByEmail(email); // lấy lại user sau khi thêm
            }

            // Kiểm tra xem user xác thực hay chưa
            if (accountExist)
            {
                if (user.IsActive == false)
                {
                    TempData["ErrorMessage"] = "Tài khoản của bạn đã bị khóa. Vui lòng liên hệ quản trị viên để biết thêm chi tiết.";
                    return RedirectToAction("Login", "Account");
                }

                if (user.IsVerify == false)
                {
                    TempData["ErrorMessage"] = "Tài khoản của bạn chưa được xác thực. Vui lòng kiểm tra email để xác thực tài khoản.";
                    return RedirectToAction("Login", "Account");
                }
            }

            // Tạo claims và đăng nhập vào hệ thống
            var claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role ?? RoleConstants.User)

            }, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(claimsIdentity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            HttpContext.Session.SetString("Username", user.Username);
            HttpContext.Session.SetString("Role", user.Role ?? RoleConstants.User);


            TempData["msg"] = $"Xin chào {user.Username}!";
            return RedirectToAction("Index", "Home");
        }
    }
}
