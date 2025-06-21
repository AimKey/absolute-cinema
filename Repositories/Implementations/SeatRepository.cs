using BusinessObjects.Models;
using DataAccessObjects;

namespace Repositories;

public class SeatRepository : GenericRepository<Seat>, ISeatRepository
{
    private readonly AbsoluteCinemaContext _context;

    public SeatRepository(AbsoluteCinemaContext context) : base(context)
    {
        _context = context;
    }
}