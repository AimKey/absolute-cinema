using Absolute_cinema.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Security.Claims;
using System.Threading.Tasks;
using Common.Constants;

namespace Absolute_cinema.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: Account/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _userService.GetUserByUserName(model.Username);

                if (user != null)
                {
                    if (user.IsActive == false)
                    {
                        TempData["ErrorMessage"] = "Tài khoản của bạn đã bị khóa. Vui lòng liên hệ quản trị viên để biết thêm chi tiết.";
                        return View(model);
                    }

                    if (user.IsVerify == false)
                    {
                        TempData["ErrorMessage"] = "Tài khoản của bạn chưa được xác thực. Vui lòng kiểm tra email để xác thực tài khoản.";
                        return View(model);
                    }

                    // Kiểm tra mật khẩu người dùng nhập có khớp với mật khẩu đã hash
                    var isPasswordCorrect = _userService.VerifyPassword(model.Password, user.Password);

                    if (isPasswordCorrect)
                    {
                        // Ghi session
                        HttpContext.Session.SetString("Username", user.Username);
                        HttpContext.Session.SetString("UserId", user.Id.ToString());
                        HttpContext.Session.SetString("Role", user.Role ?? "User");

                        // Thêm xác thực Cookie
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, user.Username),
                            new Claim(ClaimTypes.Role, user.Role ?? RoleConstants.User),
                            new Claim("UserId", user.Id.ToString())
                        };

                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var principal = new ClaimsPrincipal(identity);
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                        TempData["ErrorMessage"] = null;
                        return RedirectToAction("Index", "Home");
                    }
                }

                // Nếu sai username hoặc mật khẩu
                // ModelState.AddModelError("", "Thông tin đăng nhập không đúng.");
                TempData["ErrorMessage"] = "Thông tin đăng nhập không đúng. Vui lòng kiểm tra lại.";
            }

            // Luôn return view nếu có lỗi
            return View(model);
        }


        // GET: Account/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra username đã tồn tại
                if (_userService.IsUsernameExists(model.Username))
                {
                    ModelState.AddModelError("Username", "Tên đăng nhập đã tồn tại");
                    return View(model);
                }

                // Kiểm tra email đã tồn tại
                if (_userService.IsEmailExists(model.Email))
                {
                    ModelState.AddModelError("Email", "Email đã được sử dụng");
                    return View(model);
                }

                // Tạo tài khoản mới
                var fullName = $"{model.FirstName} {model.LastName}";
                var baseUrl = $"{Request.Scheme}://{Request.Host}";
                var result = await _userService.RegisterUserAsync(
                    model.Username,
                    model.Email,
                    model.Password,
                    fullName,
                    model.Phone,
                    model.Gender,
                    model.DateOfBirth,
                    baseUrl
                );

                if (result)
                {
                    TempData["SuccessMessage"] = "Register successfully !!! Check mail to active account";
                    return RedirectToAction("RegisterSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "Có lỗi xảy ra trong quá trình đăng ký. Vui lòng thử lại.");
                }
            }

            return View(model);
        }

        // GET: Account/RegisterSuccess
        public IActionResult RegisterSuccess()
        {
            return View();
        }

        // GET: Account/VerifyEmail
        public IActionResult VerifyEmail(string token, string email)
        {
            if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(email))
            {
                TempData["ErrorMessage"] = "Link xác thực không hợp lệ.";
                return RedirectToAction("Login");
            }

            var result = _userService.VerifyEmail(email, token);

            if (result)
            {
                TempData["SuccessMessage"] = "Xác thực email thành công! Bạn có thể đăng nhập ngay bây giờ.";
            }
            else
            {
                TempData["ErrorMessage"] = "Link xác thực không hợp lệ hoặc đã hết hạn.";
            }

            return RedirectToAction("Login");
        }

        // GET: Account/Logout
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }


        // GET: Account/ForgotPassword
        public IActionResult ForgotPassword()
        {
            return View();
        }

        // POST: Account/ForgotPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.SendOtpAsync(model.Email);

                if (result)
                {
                    TempData["SuccessMessage"] = "Mã OTP đã được gửi đến email của bạn. Vui lòng kiểm tra email.";
                    return RedirectToAction("VerifyOtp", new { email = model.Email });
                }
                else
                {
                    ModelState.AddModelError("Email", "Email không tồn tại trong hệ thống.");
                }
            }

            return View(model);
        }

        // GET: Account/VerifyOtp
        public IActionResult VerifyOtp(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("ForgotPassword");
            }

            var model = new VerifyOtpViewModel { Email = email };
            return View(model);
        }

        // POST: Account/VerifyOtp
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult VerifyOtp(VerifyOtpViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _userService.VerifyOtp(model.Email, model.Otp);

                if (result)
                {
                    TempData["SuccessMessage"] = "Mã OTP hợp lệ! Vui lòng nhập mật khẩu mới.";
                    return RedirectToAction("ResetPassword", new { email = model.Email, otp = model.Otp });
                }
                else
                {
                    ModelState.AddModelError("Otp", "Mã OTP không đúng hoặc đã hết hạn.");
                }
            }

            return View(model);
        }

        // GET: Account/ResetPassword
        public IActionResult ResetPassword(string email, string otp)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(otp))
            {
                return RedirectToAction("ForgotPassword");
            }

            var model = new ResetPasswordViewModel { Email = email, Otp = otp };
            return View(model);
        }

        // POST: Account/ResetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.ResetPasswordAsync(model.Email, model.Otp, model.NewPassword);

                if (result)
                {
                    TempData["SuccessMessage"] = "Đặt lại mật khẩu thành công! Bạn có thể đăng nhập với mật khẩu mới.";
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError("", "Mật khẩu mới trùng với mật khẩu cũ.");
                }
            }

            return View(model);
        }

        // POST: Account/SendOtpAgain
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendOtpAgain(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return Json(new { success = false, message = "Email không hợp lệ." });
            }

            var result = await _userService.SendOtpAsync(email);

            if (result)
            {
                return Json(new { success = true, message = "Mã OTP mới đã được gửi đến email của bạn." });
            }
            else
            {
                return Json(new { success = false, message = "Có lỗi xảy ra khi gửi OTP. Vui lòng thử lại." });
            }
        }
    }
}