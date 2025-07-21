using BusinessObjects.Models;
using DataAccessObjects;

namespace Repositories;

public class ReviewRepository : GenericRepository<Review>, IReviewRepository
{
    private readonly AbsoluteCinemaContext _context;

    public ReviewRepository(AbsoluteCinemaContext context) : base(context)
    {
        _context = context;
    }
}