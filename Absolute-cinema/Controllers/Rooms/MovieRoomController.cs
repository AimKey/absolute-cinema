using BusinessObjects.Models;
using Common;
using Common.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Implementations;
using Services.Interfaces;

namespace Absolute_cinema.Controllers.Rooms
{
    [Authorize]
    public class MovieRoomController : Controller
    {
        private IRoomService _roomService;
        private IShowtimeService _showtimeService;
        private IBookingService _bookingService;
        private IUserService _userService;

        public MovieRoomController(IRoomService roomService,
                                   IShowtimeService showtimeService,
                                   IBookingService bookingService,
                                   IUserService userService)
        {
            _roomService = roomService;
            _showtimeService = showtimeService;
            _bookingService = bookingService;
            _userService = userService;
        }

        public IActionResult Index(Guid showtimeId)
        {
            string msg = "Something went wrong, Please try again later ";
            string msgType = "error";
            try
            {
                var user = GetCurrentUser();
                Room room = _roomService.GetRoomFromShowtimeId(showtimeId);
                RoomSeatsVM vm = _roomService.MapRoomToRoomSeatsVM(room, showtimeId);
                return View(vm);
            }
            catch (Exception e)
            {
                var showtime = _showtimeService.GetById(showtimeId);
                var movie = showtime?.Movie;
                TempData[StatusConstants.Message] = msg + e.Message;
                TempData[StatusConstants.MessageType] = msgType;
                return RedirectToAction("Details", "Movies", new { id = movie?.Id });
            }
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
