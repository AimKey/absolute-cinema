using BusinessObjects.Models;
using Common.DTOs.TicketDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Absolute_cinema.Controllers.Tickets
{
    public class TicketsController : Controller
    {
        private readonly IUserService _userService;
        private readonly ITicketService _ticketService;
        private readonly IShowtimeSeatService _showtimeSeatService;
        private readonly IBookingService _bookingService;

        public TicketsController(IUserService userService,
                                 ITicketService ticketService,
                                 IShowtimeSeatService showtimeSeatService)
        {
            _userService = userService;
            _ticketService = ticketService;
            _showtimeSeatService = showtimeSeatService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult BookTicket(BookingTicketDTO dto)
        {
            try
            {
                var curUser = GetCurrentUser();
                if (curUser == null)
                {
                    TempData["Message"] = "You need to log in to book a ticket.";
                    TempData["MessageType"] = "error";
                    return RedirectToAction("Login", "Account");
                }

                // First, create the showtime seat
                _showtimeSeatService.InsertShowtimeSeatFromDTO(dto.ChosenSeats);
                // Then create the booking
                var bookingId = _bookingService.CreateTemporaryBookingForUser(curUser.Id);
                // Create the tickets for the booking
                _ticketService.CreateTicketsForUserBookingFromDto(dto, bookingId, curUser.Id);
                // Calculate the booking for user
                Booking booking = _bookingService.CalculateBookingForUser(bookingId, curUser.Id);
                // TODO: Locking seat


                // Redirect to booking controller to view the booking details
                return RedirectToAction(
                    "Details", "Bookings", new { bookingId = booking.Id, userId = curUser.Id });

            }
            catch (Exception e)
            {

                throw;
            }
        }

        private User GetCurrentUser()
        {
            var userNameString = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(userNameString))
            {
                return null; // or handle the case where the user is not logged in
            }
            var user = _userService.GetUserByUserName(userNameString);
            return user;
        }
    }
}
