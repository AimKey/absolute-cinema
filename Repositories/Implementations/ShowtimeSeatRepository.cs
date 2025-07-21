using BusinessObjects.Models;
using DataAccessObjects;

namespace Repositories;

public class ShowtimeSeatRepository : GenericRepository<ShowtimeSeat>, IShowtimeSeatRepository
{
    private readonly AbsoluteCinemaContext _context;

    public ShowtimeSeatRepository(AbsoluteCinemaContext context) : base(context)
    {
        _context = context;
    }
}