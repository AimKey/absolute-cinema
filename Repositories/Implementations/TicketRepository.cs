using BusinessObjects.Models;
using DataAccessObjects;

namespace Repositories;

public class TicketRepository : GenericRepository<Ticket>, ITicketRepository
{
    private readonly AbsoluteCinemaContext _context;

    public TicketRepository(AbsoluteCinemaContext context) : base(context)
    {
        _context = context;
    }
}