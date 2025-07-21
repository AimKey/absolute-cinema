using BusinessObjects.Models;

namespace Services.Interfaces;

public interface IUserDetailService
{
    IEnumerable<UserDetail> GetAll();
    UserDetail GetById(Guid id);
    void Add(UserDetail userDetail);
    void Update(UserDetail userDetail);
    void Delete(Guid id);
    void Delete(UserDetail userDetail);
} 