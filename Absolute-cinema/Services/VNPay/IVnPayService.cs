using Absolute_cinema.Models.VNPay;

namespace Absolute_cinema.Services.VNPay
{
    public interface IVnPayService
    {
        string CreatePaymentUrl(PaymentInformationModel model, HttpContext context);
        PaymentResponseModel PaymentExecute(IQueryCollection collections);


    }
}
