using BusinessObjects.Models;

namespace Services.Interfaces;

public interface IUserService
{
    List<User> GetAll();
    User GetById(Guid id);
    void Add(User user);
    void Update(UserDetail userDetail);
    void Delete(UserDetail userDetail);
    User GetUserByUserName(string username);
}