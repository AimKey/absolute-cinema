namespace Services.Interfaces;

public interface IEmailService
{
    Task SendEmailAsync(string toEmail, string subject, string message);
    Task SendVerificationEmailAsync(string toEmail, string userName, string verificationLink);
}
