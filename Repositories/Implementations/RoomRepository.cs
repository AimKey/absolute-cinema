using BusinessObjects.Models;
using DataAccessObjects;

namespace Repositories;

public class RoomRepository : GenericRepository<Room>, IRoomRepository
{
    private readonly AbsoluteCinemaContext _context;

    public RoomRepository(AbsoluteCinemaContext context) : base(context)
    {
        _context = context;
    }
}