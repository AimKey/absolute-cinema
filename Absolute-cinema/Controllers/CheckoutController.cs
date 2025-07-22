using Common.DTOs.PaymentDTOs;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Services.VNPay;

namespace Absolute_cinema.Controllers
{
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
            List<string> strings = response.OrderDescription.Split(",").ToList();

            Guid bookingId = new Guid(strings[0]);
            Guid userId = new Guid(strings[1]);
            int ticketsCount = Int32.Parse((strings[2])); 
            List<string> finalString = strings[3].Split(" ").ToList();
            string movieTitle = finalString[0];
            decimal amount = Decimal.Parse(finalString[1]);

            var paymentDto = new VNPayPaymentDTO
            {
                BookingId = bookingId,
                UserId = userId,
                TicketsCount = ticketsCount,
                MovieTitle = movieTitle,
                Amount = amount
            };
            var payment = _paymentService.CreatePaymentFromDTO(paymentDto);
            _bookingService.BookingFinished(bookingId, payment.Id);

            if (response.VnPayResponseCode == "00") {
                return View("PaymentSuccess", response);
            }
            else
            {
                return View("PaymentFailed", response);
            }
        }
    }
} 