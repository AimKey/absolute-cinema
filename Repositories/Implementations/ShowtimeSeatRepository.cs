using BusinessObjects.Models;
using DataAccessObjects;

namespace Repositories;

public class ShowtimeSeatRepository : GenericRepository<Showtime>, IShowtimeSeatRepository
{
    private readonly AbsoluteCinemaContext _context;

    public ShowtimeSeatRepository(AbsoluteCinemaContext context) : base(context)
    {
        _context = context;
    }
}