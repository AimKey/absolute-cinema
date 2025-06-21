using BusinessObjects.Models;
using DataAccessObjects;

namespace Repositories;

public class BookingRepository : GenericRepository<Booking>, IBookingRepository
{
    private readonly AbsoluteCinemaContext _context;

    public BookingRepository(AbsoluteCinemaContext context) : base(context)
    {
        _context = context;
    }
}