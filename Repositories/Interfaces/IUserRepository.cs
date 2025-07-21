using BusinessObjects.Models;

namespace Repositories;

public interface IUserRepository : IGenericRepository<User>
{
    User GetUserByUserName(string userName);
}