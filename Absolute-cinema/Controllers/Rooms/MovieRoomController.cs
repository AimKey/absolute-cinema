using BusinessObjects.Models;
using Common;
using Common.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Absolute_cinema.Controllers.Rooms
{
    public class MovieRoomController : Controller
    {
        private IRoomService _roomService;
        private IShowtimeService _showtimeService;

        public MovieRoomController(IRoomService roomService,
                                   IShowtimeService showtimeService)
        {
            _roomService = roomService;
            _showtimeService = showtimeService;
        }

        public IActionResult Index(Guid showtimeId)
        {
            string msg = "Something went wrong, Please try again later ";
            string msgType = "error";
            try
            {
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
    }
}
