using Common.DTOs.VNPay;
using Microsoft.AspNetCore.Mvc;
using Services.VNPay;

namespace Absolute_cinema.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly IVnPayService _vnPayService;
        public CheckoutController(IVnPayService vnPayService)
        {

            _vnPayService = vnPayService;
        }

        [HttpGet]
        public IActionResult PaymentCallbackVnpay()
        {
            var response = _vnPayService.PaymentExecute(Request.Query);
            return Json(response);
        }
    }
}
