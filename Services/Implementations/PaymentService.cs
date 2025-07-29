using BusinessObjects.Models;
using Common.DTOs.PaymentDTOs;
using Repositories;
using Services.Interfaces;

namespace Services.Implementations;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _paymentRepository;

    public PaymentService(IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    public Payment CreatePaymentFromDTO(VNPayPaymentDTO dto)
    {
        if (dto == null)
        {
            throw new ArgumentNullException(nameof(dto), "Payment DTO cannot be null");
        }
        var payment = new Payment
        {
            Id = Guid.NewGuid(),
            BookingId = dto.BookingId,
            Amount = (int)(dto.Amount * 100), // Assuming Amount is in VND and needs to be in cents
            OrderCode = 1, // TODO: Implement a method to generate unique order codes
            Description = $"Payment for {dto.MovieTitle} - {dto.TicketsCount} tickets",
            TransactionDateTime = DateTime.Now,
            PaymentMethod = "Online banking",
            Status = "Success",
            Currency = "VND" // Assuming VND as the currency
        };
        Add(payment);
        return payment;
    }

    public IEnumerable<Payment> GetAll()
    {
        return _paymentRepository.Get().ToList();
    }

    public Payment GetById(Guid id)
    {
        return _paymentRepository.GetByID(id);
    }

    public void Add(Payment payment)
    {
        _paymentRepository.Insert(payment);
        _paymentRepository.Save();
    }

    public void Update(Payment payment)
    {
        _paymentRepository.Update(payment);
        _paymentRepository.Save();
    }

    public void Delete(Guid id)
    {
        _paymentRepository.Delete(id);
        _paymentRepository.Save();
    }

    public void Delete(Payment payment)
    {
        _paymentRepository.Delete(payment);
        _paymentRepository.Save();
    }
} 