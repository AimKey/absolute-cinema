using BusinessObjects.Models;
using Repositories;
using Services.Interfaces;

namespace Services.Implementations;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUserDetailRepository _userDetailRepository;

    public UserService(IUserRepository userRepository, IUserDetailRepository userDetailRepository)
    {
        _userRepository = userRepository;
        _userDetailRepository = userDetailRepository;
    }

    public User GetUserByUserName(string username)
    {
        return _userRepository.GetUserByUserName(username);
    }

    public List<User> GetAll()
    {
        return _userRepository.Get().ToList();
    }

    public User GetById(Guid id)
    {
        return _userRepository.GetByID(id);
    }

    public void Add(User user)
    {
        _userRepository.Insert(user);
        _userRepository.Save();
    }

    public void Update(UserDetail userDetail)
    {
        _userDetailRepository.Update(userDetail);
        _userDetailRepository.Save();
    }

    public void Delete(UserDetail userDetail)
    {
        _userDetailRepository.Delete(userDetail);
        _userDetailRepository.Save();
    }
}