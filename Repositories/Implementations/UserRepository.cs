using BusinessObjects.Models;
using DataAccessObjects;

namespace Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    private readonly AbsoluteCinemaContext _context;

    public UserRepository(AbsoluteCinemaContext context) : base(context)
    {
        _context = context;
    }

    public User GetUserByUserName(string userName)
    {
        var user = Get(u => u.Username == userName).FirstOrDefault();
        return user;
    }
}