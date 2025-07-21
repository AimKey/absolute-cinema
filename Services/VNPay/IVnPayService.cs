using Common.DTOs.VNPay;
using Microsoft.AspNetCore.Http;

namespace Services.VNPay;

public interface IVnPayService
{
    string CreatePaymentUrl(PaymentInformationModel model, HttpContext context);
    PaymentResponseModel PaymentExecute(IQueryCollection collections);

}
