using BusinessObjects.Models;
using DataAccessObjects;

namespace Repositories;

public class MovieDirectorRepository : GenericRepository<MovieDirector>, IMovieDirectorRepository
{
    private readonly AbsoluteCinemaContext _context;

    public MovieDirectorRepository(AbsoluteCinemaContext context) : base(context)
    {
        _context = context;
    }
}