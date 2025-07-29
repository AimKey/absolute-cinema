
using Common.DTOs.VNPay;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.VNPay;

namespace Absolute_cinema.Controllers
{
    [Authorize]
    public class PaymentController : Controller
    {
        private readonly IVnPayService _vnPayService;
        private readonly ILogger<VnPayService> _logger;
        public PaymentController(IVnPayService vnPayService, ILogger<VnPayService> logger)
        {

            _vnPayService = vnPayService;
            _logger = logger;
        }

        public IActionResult CreatePaymentUrlVnpay(PaymentInformationModel model)
        {
            var url = _vnPayService.CreatePaymentUrl(model, HttpContext);
            _logger.LogInformation("Generated VNPay payment URL: {Url}", url);

            return Redirect(url);
        }
    }
}
