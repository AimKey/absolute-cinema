using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using Services.Interfaces;
using Common.Models;

namespace Services.Implementations
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            try
            {
                using var client = new SmtpClient(_emailSettings.SmtpHost, _emailSettings.SmtpPort);
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(_emailSettings.SmtpUser, _emailSettings.SmtpPass);

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_emailSettings.FromEmail, _emailSettings.FromName),
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(toEmail);

                await client.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error sending email: {ex.Message}", ex);
            }
        }

        public async Task SendVerificationEmailAsync(string toEmail, string userName, string verificationLink)
        {
            var subject = "Xác thực tài khoản - Absolute Cinema";
            var message = $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <style>
        body {{ font-family: Arial, sans-serif; background-color: #f4f4f4; margin: 0; padding: 20px; }}
        .container {{ max-width: 600px; margin: 0 auto; background-color: white; border-radius: 10px; overflow: hidden; box-shadow: 0 4px 6px rgba(0,0,0,0.1); }}
        .header {{ background: linear-gradient(135deg, #1e3c72 0%, #2a5298 100%); color: white; padding: 30px; text-align: center; }}
        .logo {{ font-size: 28px; font-weight: bold; margin-bottom: 10px; }}
        .content {{ padding: 30px; }}
        .button {{ display: inline-block; background: linear-gradient(135deg, #667eea 0%, #764ba2 100%); color: white; text-decoration: none; padding: 12px 30px; border-radius: 5px; font-weight: bold; margin: 20px 0; }}
        .footer {{ background-color: #f8f9fa; padding: 20px; text-align: center; color: #6c757d; font-size: 14px; }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <div class='logo'>🎬 ABSOLUTE CINEMA</div>
            <p>Xác thực tài khoản của bạn</p>
        </div>
        <div class='content'>
            <h2>Chào mừng {userName}!</h2>
            <p>Cảm ơn bạn đã đăng ký tài khoản tại Absolute Cinema. Để hoàn tất quá trình đăng ký, vui lòng nhấp vào nút bên dưới để xác thực email của bạn:</p>
            
            <div style='text-align: center;'>
                <a href='{verificationLink}' class='button'>Xác thực tài khoản</a>
            </div>
            
            <p>Hoặc copy và paste link sau vào trình duyệt:</p>
            <p style='word-break: break-all; background-color: #f8f9fa; padding: 10px; border-radius: 5px;'>{verificationLink}</p>
            
            <p><strong>Lưu ý:</strong> Link xác thực này sẽ hết hạn sau 24 giờ.</p>
            
            <p>Nếu bạn không tạo tài khoản này, vui lòng bỏ qua email này.</p>
            
            <hr style='margin: 30px 0; border: none; border-top: 1px solid #eee;'>
            
            <h3>🎬 Tại sao chọn Absolute Cinema?</h3>
            <ul>
                <li>✅ Hệ thống âm thanh Dolby Atmos cao cấp</li>
                <li>✅ Màn hình IMAX với chất lượng 4K</li>
                <li>✅ Ghế ngồi VIP thoải mái và sang trọng</li>
                <li>✅ Đặt vé online nhanh chóng và tiện lợi</li>
                <li>✅ Ưu đãi đặc biệt cho thành viên</li>
            </ul>
        </div>
        <div class='footer'>
            <p>© 2025 Absolute Cinema. All rights reserved.</p>
            <p>Email này được gửi tự động, vui lòng không reply.</p>
        </div>
    </div>
</body>
</html>";

            await SendEmailAsync(toEmail, subject, message);
        }
    }
}
