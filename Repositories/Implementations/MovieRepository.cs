using BusinessObjects.Models;
using DataAccessObjects;

namespace Repositories;

public class MovieRepository : GenericRepository<Movie>, IMovieRepository
{
    private readonly AbsoluteCinemaContext _context;

    public MovieRepository(AbsoluteCinemaContext context) : base(context)
    {
        _context = context;
    }
}