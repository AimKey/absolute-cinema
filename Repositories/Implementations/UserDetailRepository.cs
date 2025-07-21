using BusinessObjects.Models;
using DataAccessObjects;

namespace Repositories;

public class UserDetailRepository : GenericRepository<UserDetail>, IUserDetailRepository
{
    public UserDetailRepository(AbsoluteCinemaContext context) : base(context)
    {
    }
}