using App.Domain.Models.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace App.Infrastructure.Authorization.Interfaces
{
    public interface IBearerTokenService
    {

        Task<JwtSecurityToken> GenerateJwtTokenAsync(User user, bool isRefreshToken = false);
        bool ValidateToken(string token, bool isRefreshToken = false);
        string GetUserId(string token);
        string[] GetUserRole(string token);
    }
}
