using BusinessObjects.Models;
using Common.DTOs.TicketDTOs;

namespace Services.Interfaces;

public interface IUserService
{
    List<User> GetAll();
    User GetById(Guid id);
    void Add(User user);
    void Update(UserDetail userDetail);
    void Delete(UserDetail userDetail);
    User GetUserByUserName(string username);
    User? GetUserByEmail(string email);
    bool IsUsernameExists(string username);
    bool IsEmailExists(string email);
    Task<bool> RegisterUserAsync(string username, string email, string password, string fullName, string phone, string gender, DateTime dateOfBirth, string baseUrl);
    bool VerifyEmail(string email, string token);
    void UpdateUser(User user);
    
    // Forgot Password methods
    Task<bool> SendOtpAsync(string email);
    bool VerifyOtp(string email, string otp);
    Task<bool> ResetPasswordAsync(string email, string otp, string newPassword);
}