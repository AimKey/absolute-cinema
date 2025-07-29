using BusinessObjects.Models;
using Common;
using Common.DTOs.PaymentDTOs;
using Common.DTOs.VNPay;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Services.VNPay;

namespace Absolute_cinema.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        private readonly IVnPayService _vnPayService;
        private readonly IBookingService _bookingService;
        private readonly IPaymentService _paymentService;
        public CheckoutController(IVnPayService vnPayService, IBookingService bookingService, IPaymentService paymentService)
        {
            _vnPayService = vnPayService;
            _bookingService = bookingService;
            _paymentService = paymentService;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult PaymentCallbackVnpay()
        {
            var response = _vnPayService.PaymentExecute(Request.Query);
            List<string> responses = response.OrderDescription.Trim().Split(" ").ToList();

            string bookingInfoString = responses[0];
            string amountString = responses[1];

            Guid bookingId = new Guid(bookingInfoString);
            decimal amount = Decimal.Parse(amountString);

            Booking booking = _bookingService.GetById(bookingId);
            User u = booking.User;
            int ticketsCount = booking.NumberOfTickets;
            string movieTitle = booking.Tickets.FirstOrDefault().ShowtimeSeat.Showtime.Movie.Title;

            var paymentDto = new VNPayPaymentDTO
            {
                BookingId = bookingId,
                UserId = u.Id,
                TicketsCount = ticketsCount,
                MovieTitle = movieTitle,
                Amount = amount
            };

            try
            {
                response.OrderDescription = $"{ticketsCount} for {movieTitle}. Total amount: {amount}";
                if (response.VnPayResponseCode == "00")
                {
                    var payment = _paymentService.CreatePaymentFromDTO(paymentDto);
                    _bookingService.BookingFinished(bookingId, payment.Id);
                    return View("~/Views/Payments/PaymentSuccess.cshtml", response);
                }
                else
                {
                    _bookingService.CancelBooking(bookingId);
                    return View("~/Views/Payments/PaymentError.cshtml", response);
                }

            }
            catch (Exception e)
            {
                TempData["msg"] = $"An error has occured: {e.Message}";
                TempData["msgType"] = StatusConstants.Error;
                _bookingService.CancelBooking(bookingId);
                return View("~/Views/Payments/PaymentError.cshtml", response);
            }
        }
    }
}