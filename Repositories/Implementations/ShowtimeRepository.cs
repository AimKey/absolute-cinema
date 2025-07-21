using BusinessObjects.Models;
using DataAccessObjects;

namespace Repositories;

public class ShowtimeRepository : GenericRepository<Showtime>, IShowtimeRepository
{
    private readonly AbsoluteCinemaContext _context;

    public ShowtimeRepository(AbsoluteCinemaContext context) : base(context)
    {
        _context = context;
    }
}