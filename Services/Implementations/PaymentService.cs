using BusinessObjects.Models;
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