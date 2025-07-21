using BusinessObjects.Models;
using Repositories;
using Services.Interfaces;

namespace Services.Implementations;

public class UserDetailService : IUserDetailService
{
    private readonly IUserDetailRepository _userDetailRepository;

    public UserDetailService(IUserDetailRepository userDetailRepository)
    {
        _userDetailRepository = userDetailRepository;
    }

    public IEnumerable<UserDetail> GetAll()
    {
        return _userDetailRepository.Get().ToList();
    }

    public UserDetail GetById(Guid id)
    {
        return _userDetailRepository.GetByID(id);
    }

    public void Add(UserDetail userDetail)
    {
        _userDetailRepository.Insert(userDetail);
        _userDetailRepository.Save();
    }

    public void Update(UserDetail userDetail)
    {
        _userDetailRepository.Update(userDetail);
        _userDetailRepository.Save();
    }

    public void Delete(Guid id)
    {
        _userDetailRepository.Delete(id);
        _userDetailRepository.Save();
    }

    public void Delete(UserDetail userDetail)
    {
        _userDetailRepository.Delete(userDetail);
        _userDetailRepository.Save();
    }
} 