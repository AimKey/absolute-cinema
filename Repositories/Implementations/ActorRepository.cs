using BusinessObjects.Models;
using DataAccessObjects;

namespace Repositories;

public class ActorRepository : GenericRepository<Actor>, IActorRepository
{
    private readonly AbsoluteCinemaContext _context;

    public ActorRepository(AbsoluteCinemaContext context) : base(context)
    {
        _context = context;
    }
}