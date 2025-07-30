using BusinessObjects.Models;
using Common;
using Common.Constants;
using Common.ViewModels.BookingVMs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Absolute_cinema.Controllers
{
    [Authorize]
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
        public IActionResult Index(int page = 1, int pageSize = -1)
        {
            if (pageSize == -1)
            {
                pageSize = PageConstants.PageSize;
            }
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

            // Simple pagination
            var totalItems = bookingViewModels.Count;
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            // Ensure page is within valid range
            page = Math.Max(1, Math.Min(page, totalPages > 0 ? totalPages : 1));

            bookingViewModels = bookingViewModels
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalItems = totalItems;

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
            ReviewBookingVM reviewBooking = _bookingService.GetReviewBookingVM(bookingId, curUser.Id);

            return View(reviewBooking);
        }

        [HttpPost]
        public IActionResult CancelBooking(Guid bookingId)
        {
            try
            {
                _bookingService.CancelBooking(bookingId);
                TempData["msg"] = "Cancel booking completed";
                TempData["msgType"] = StatusConstants.Success;
            }
            catch (Exception e)
            {
                TempData["msg"] = "Cancel booking failed: " + e.Message;
                TempData["msgType"] = StatusConstants.Error;
            }
            return RedirectToAction(nameof(Index));
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
