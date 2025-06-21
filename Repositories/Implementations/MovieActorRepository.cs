using BusinessObjects.Models;
using DataAccessObjects;

namespace Repositories;

public class MovieActorRepository : GenericRepository<MovieActor>, IMovieActorRepository
{
    private readonly AbsoluteCinemaContext _context;

    public MovieActorRepository(AbsoluteCinemaContext context) : base(context)
    {
        _context = context;
    }
}