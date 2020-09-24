using App.Domain.Utils.Settings;
using App.Infrastructure.Authorization.Enums;
using App.Infrastructure.Authorization.Models.Login;
using App.Infrastructure.Authorization.Models.Registration;
using App.Infrastructure.Authorization.Models.Response;
using System.Threading.Tasks;

namespace App.Infrastructure.Authorization.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse> LoginAsync(Login model);
        Task<AuthResponse> RegisterAsync(Register model, UserRole role);
        Task<AuthResponse> ChangePasswordAsync(ChangePassword model);
        Task<UserSettings> GetUserClaimsAsync(string token);
        Task<string> GetForgotPasswordTokenAsync(ForgotPassword model);
        Task<AuthResponse> RefreshTokenAsync(string refreshToken);
    }
}
