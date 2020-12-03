using Blog.API.ViewModels;

namespace Blog.API.Services
{
    public interface IAuthService
    {
        AuthData GetAuthData(string id, string email, string username);

        string HashPassword(string password);

        bool VerifyPassword(string actualPassword, string hashedPassword);
    }
}