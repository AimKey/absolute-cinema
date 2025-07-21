using BusinessObjects.Models;
using DataAccessObjects;

namespace Repositories;

public class DirectorRepository : GenericRepository<Director>, IDirectorRepository
{
    private readonly AbsoluteCinemaContext _context;

    public DirectorRepository(AbsoluteCinemaContext context) : base(context)
    {
        _context = context;
    }
}