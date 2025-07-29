using BusinessObjects.Models;
using Common.ViewModels.BookingVMs;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Absolute_cinema.Controllers
{
    public class BookingsController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly IUserService _userService;
        private readonly IReviewService _reviewService;

        public BookingsController(IUserService userService, IBookingService bookingService, IReviewService reviewService)
        {
            _userService = userService;
            _bookingService = bookingService;
            _reviewService = reviewService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            // List all bookings of user
            var user = GetCurrentUser();
            if (user == null)
            {
                TempData["Message"] = "You need to log in to view your bookings.";
                TempData["MessageType"] = "error";
                return RedirectToAction("Login", "Account");
            }
            var bookings = _bookingService.GetBookingsByUserId(user.Id);
            var allReviews = _reviewService.GetAll();

            var bookingViewModels = bookings
                .OrderByDescending(b => b.BookingDate)
                .Select(b =>
                {
                    var movieId = b.Tickets.FirstOrDefault()?.ShowtimeSeat?.Showtime?.MovieId;
                    var hasFeedback = allReviews.Any(r => r.UserId == GetCurrentUser().Id && r.MovieId == movieId);

                    return new BookingViewModel
                    {
                        Booking = b,
                        MovieId = movieId,
                        HasFeedback = hasFeedback
                    };
                })
                .ToList();


            return View(bookingViewModels);
        }

        [HttpGet]
        public IActionResult BookingDetails(Guid bookingId)
        {
            // Get booking details by bookingId
            var booking = _bookingService.GetById(bookingId);
            if (booking == null)
            {
                TempData["Message"] = "Booking not found.";
                TempData["MessageType"] = "error";
                return RedirectToAction("Index");
            }
            var curUser = GetCurrentUser();
            if (curUser == null || curUser.Id != booking.UserId)
            {
                TempData["Message"] = "You do not have permission to view this booking.";
                TempData["MessageType"] = "error";
                return RedirectToAction("Index");
            }
            var reviewBooking = _bookingService.GetReviewBookingVM(bookingId, curUser.Id);
            return View(reviewBooking);
        }

        public IActionResult ReviewBooking(Guid bookingId, Guid? userId = null)
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
