using BusinessObjects.Models;
using DataAccessObjects;

namespace Repositories;

public class TagRepository : GenericRepository<Tag>, ITagRepository
{
    private readonly AbsoluteCinemaContext _context;

    public TagRepository(AbsoluteCinemaContext context) : base(context)
    {
        _context = context;
    }
}