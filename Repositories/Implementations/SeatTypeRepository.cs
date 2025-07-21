using BusinessObjects.Models;
using DataAccessObjects;

namespace Repositories;

public class SeatTypeRepository : GenericRepository<SeatType>, ISeatTypeRepository
{
    private readonly AbsoluteCinemaContext _context;

    public SeatTypeRepository(AbsoluteCinemaContext context) : base(context)
    {
        _context = context;
    }
}