namespace Services.Helpers
{
    public interface IHashPasswordService
    {
        string HashPassword(string password);
        bool VerifyPassword(string providePassword, string existingPassword);
    }
}