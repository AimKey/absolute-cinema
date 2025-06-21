using BusinessObjects.Models;

namespace Services.Interfaces;

public interface IPaymentService
{
    IEnumerable<Payment> GetAll();
    Payment GetById(Guid id);
    void Add(Payment payment);
    void Update(Payment payment);
    void Delete(Guid id);
    void Delete(Payment payment);
} 