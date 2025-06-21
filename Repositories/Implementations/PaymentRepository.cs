using BusinessObjects.Models;
using DataAccessObjects;

namespace Repositories;

public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
{
    private readonly AbsoluteCinemaContext _context;

    public PaymentRepository(AbsoluteCinemaContext context) : base(context)
    {
        _context = context;
    }
}