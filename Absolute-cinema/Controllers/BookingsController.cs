using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Absolute_cinema.Controllers
{
    public class BookingsController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly IUserService _userService;

        public BookingsController(IUserService userService, IBookingService bookingService)
        {
            _userService = userService;
            _bookingService = bookingService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(Guid bookingId, Guid? userId = null)
        {
            User curUser;
            if (userId == null)
            {
                curUser = GetCurrentUser();
            }
            else
            {
                curUser = _userService.GetById(userId.Value);
            }
            var reviewBooking = _bookingService.GetReviewBookingVM(bookingId, curUser.Id);

            return View(reviewBooking);
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
    }
}
