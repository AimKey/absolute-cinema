using BusinessObjects.Models;
using DataAccessObjects;

namespace Repositories;

public class MovieTagRepository : GenericRepository<MovieTag>, IMovieTagRepository
{
    private readonly AbsoluteCinemaContext _context;

    public MovieTagRepository(AbsoluteCinemaContext context) : base(context)
    {
        _context = context;
    }
}