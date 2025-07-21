using BusinessObjects.Models;
using Repositories;
using Services.Interfaces;
using System.Security.Cryptography;
using System.Text;
using Common.Constants;

namespace Services.Implementations;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUserDetailRepository _userDetailRepository;
    private readonly IEmailService _emailService;

    public UserService(IUserRepository userRepository, IUserDetailRepository userDetailRepository, IEmailService emailService)
    {
        _userRepository = userRepository;
        _userDetailRepository = userDetailRepository;
        _emailService = emailService;
    }

    public User GetUserByUserName(string username)
    {
        return _userRepository.GetUserByUserName(username);
    }

    public User? GetUserByEmail(string email)
    {
        return _userRepository.Get(u => u.Email == email).FirstOrDefault();
    }

    public bool IsUsernameExists(string username)
    {
        return _userRepository.Get(u => u.Username == username).Any();
    }

    public bool IsEmailExists(string email)
    {
        return _userRepository.Get(u => u.Email == email).Any();
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

    public void UpdateUser(User user)
    {
        _userRepository.Update(user);
        _userRepository.Save();
    }

    public void Delete(UserDetail userDetail)
    {
        _userDetailRepository.Delete(userDetail);
        _userDetailRepository.Save();
    }

    public async Task<bool> RegisterUserAsync(string username, string email, string password, string fullName, string phone, string gender, DateTime dateOfBirth, string baseUrl)
    {
        try
        {
            // Kiểm tra username và email đã tồn tại chưa
            if (IsUsernameExists(username))
                return false;

            if (IsEmailExists(email))
                return false;

            // Tạo user mới
            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = username,
                Email = email,
                Password = HashPassword(password),
                Role = RoleConstants.Customer,
                IsActive = true,
                IsVerify = false,
                CreatedAt = DateTime.Now
            };

            // Lưu user
            _userRepository.Insert(user);
            _userRepository.Save();

            // Tạo user detail
            var userDetail = new UserDetail
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                FullName = fullName,
                Phone = phone,
                Gender = gender,
                Dob = DateOnly.FromDateTime(dateOfBirth),
                CreatedAt = DateTime.Now
            };

            // Lưu user detail
            _userDetailRepository.Insert(userDetail);
            _userDetailRepository.Save();

            // Tạo verification token và gửi email
            var verificationToken = GenerateVerificationToken(email);
            var verificationLink = $"{baseUrl}/Account/VerifyEmail?token={Uri.EscapeDataString(verificationToken)}&email={Uri.EscapeDataString(email)}";
            
            await _emailService.SendVerificationEmailAsync(email, fullName, verificationLink);

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool VerifyEmail(string email, string token)
    {
        try
        {
            // Verify token
            if (!VerifyToken(email, token))
                return false;

            var user = GetUserByEmail(email);
            if (user == null)
                return false;

            user.IsVerify = true;
            user.UpdatedAt = DateTime.Now;
            
            UpdateUser(user);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    private string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(hashedBytes);
    }

    private string GenerateVerificationToken(string email)
    {
        // Use email + fixed secret instead of timestamp for simple verification
        var data = $"{email}:AbsoluteCinemaSecret2024";
        using var sha256 = SHA256.Create();
        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(data));
        return Convert.ToBase64String(hashedBytes);
    }

    private bool VerifyToken(string email, string token)
    {
        // Simple token verification - compare with expected token
        try
        {
            // URL decode token in case it was encoded
            var decodedToken = Uri.UnescapeDataString(token);
            var expectedToken = GenerateVerificationToken(email);
            return decodedToken == expectedToken;
        }
        catch (Exception)
        {
            return false;
        }
    }

    // Dictionary để lưu OTP tạm thời (trong production nên dùng Redis hoặc database)
    private static readonly Dictionary<string, (string otp, DateTime expiry)> _otpStorage = new();

    public async Task<bool> SendOtpAsync(string email)
    {
        try
        {
            // Kiểm tra email có tồn tại không
            var user = GetUserByEmail(email);
            if (user == null)
                return false;

            // Generate OTP 6 số
            var random = new Random();
            var otp = random.Next(100000, 999999).ToString();
            
            // Lưu OTP với thời gian hết hạn 1 phút
            var expiry = DateTime.Now.AddMinutes(1);
            _otpStorage[email] = (otp, expiry);

            // Gửi email OTP
            var subject = "Mã OTP đặt lại mật khẩu - Absolute Cinema";
            var message = $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <style>
        body {{ font-family: Arial, sans-serif; background-color: #f4f4f4; margin: 0; padding: 20px; }}
        .container {{ max-width: 600px; margin: 0 auto; background-color: white; border-radius: 10px; overflow: hidden; box-shadow: 0 4px 6px rgba(0,0,0,0.1); }}
        .header {{ background: linear-gradient(135deg, #667eea 0%, #764ba2 100%); color: white; padding: 30px; text-align: center; }}
        .logo {{ font-size: 28px; font-weight: bold; margin-bottom: 10px; }}
        .content {{ padding: 30px; text-align: center; }}
        .otp-code {{ font-size: 36px; font-weight: bold; color: #667eea; background: #f8f9fa; padding: 20px; border-radius: 10px; margin: 20px 0; letter-spacing: 5px; }}
        .warning {{ color: #dc3545; font-weight: bold; margin: 20px 0; }}
        .footer {{ background-color: #f8f9fa; padding: 20px; text-align: center; color: #6c757d; font-size: 14px; }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <div class='logo'>🎬 ABSOLUTE CINEMA</div>
            <p>Đặt lại mật khẩu</p>
        </div>
        <div class='content'>
            <h2>Mã OTP của bạn</h2>
            <p>Sử dụng mã OTP dưới đây để đặt lại mật khẩu:</p>
            
            <div class='otp-code'>{otp}</div>
            
            <div class='warning'>
                ⚠️ Mã OTP này sẽ hết hạn sau 1 phút
            </div>
            
            <p>Nếu bạn không yêu cầu đặt lại mật khẩu, vui lòng bỏ qua email này.</p>
        </div>
        <div class='footer'>
            <p>© 2025 Absolute Cinema. All rights reserved.</p>
            <p>Email này được gửi tự động, vui lòng không reply.</p>
        </div>
    </div>
</body>
</html>";

            await _emailService.SendEmailAsync(email, subject, message);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool VerifyOtp(string email, string otp)
    {
        try
        {
            if (!_otpStorage.ContainsKey(email))
                return false;

            var (storedOtp, expiry) = _otpStorage[email];
            
            // Kiểm tra hết hạn
            if (DateTime.Now > expiry)
            {
                _otpStorage.Remove(email);
                return false;
            }

            // Kiểm tra OTP đúng
            if (storedOtp != otp)
                return false;

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public Task<bool> ResetPasswordAsync(string email, string otp, string newPassword)
    {
        try
        {
            // Verify OTP một lần nữa
            if (!VerifyOtp(email, otp))
                return Task.FromResult(false);

            var user = GetUserByEmail(email);
            if (user == null)
                return Task.FromResult(false);

            // Kiểm tra mật khẩu mới không trùng mật khẩu cũ
            var newPasswordHash = HashPassword(newPassword);
            if (user.Password == newPasswordHash)
                return Task.FromResult(false); // Mật khẩu mới trùng mật khẩu cũ

            // Cập nhật mật khẩu mới
            user.Password = newPasswordHash;
            user.UpdatedAt = DateTime.Now;
            
            UpdateUser(user);

            // Xóa OTP sau khi sử dụng
            _otpStorage.Remove(email);

            return Task.FromResult(true);
        }
        catch (Exception)
        {
            return Task.FromResult(false);
        }
    }
}