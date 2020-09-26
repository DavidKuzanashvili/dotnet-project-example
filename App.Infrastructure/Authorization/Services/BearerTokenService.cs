using App.Domain.Entities.Users;
using App.Infrastructure.Authorization.Interfaces;
using App.Infrastructure.Authorization.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.Authorization.Services
{
    public class BearerTokenService : IBearerTokenService
    {
        private readonly JWTSettings _jwtSettings;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<BearerTokenService> _logger;

        public BearerTokenService(
            IOptions<JWTSettings> jwtSettings,
            UserManager<User> userManager,
            TokenValidationParameters tokenValidationParameters,
            ILogger<BearerTokenService> logger)
        {
            _jwtSettings = jwtSettings.Value;
            _userManager = userManager;
            _tokenValidationParameters = tokenValidationParameters;
            _logger = logger;
        }

        public async Task<JwtSecurityToken> GenerateJwtTokenAsync(User user, bool isRefreshToken = false)
        {
            var claims = await GetUserClaimsAsync(user);
            var expirationTime = isRefreshToken ? _jwtSettings.RefreshExpireMinute : _jwtSettings.ExpireMinute;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMinutes(Convert.ToDouble(expirationTime));

            var token = new JwtSecurityToken(
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            return token;
        }

        public bool ValidateToken(string token, bool isRefreshToken = false)
        {
            try
            {
                var tokenKey = isRefreshToken ? _jwtSettings.RefreshKey : _jwtSettings.Key;
                var key = Encoding.UTF8.GetBytes(tokenKey);

                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwtToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
                if (jwtToken == null)
                    return false;

                ClaimsPrincipal principal = tokenHandler.ValidateToken(token,
                      _tokenValidationParameters, out SecurityToken securityToken);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return false;
            }
        }

        public string GetUserId(string token)
        {
            var tokenData = ReadTokenData(token);
            var userId = tokenData.Claims
                .Where(x => x.Type == JwtRegisteredClaimNames.UniqueName)
                .Select(x => x.Value)
                .FirstOrDefault();

            return userId;
        }

        public string[] GetUserRoles(string token)
        {
            var tokenData = ReadTokenData(token);
            var role = tokenData.Claims.Where(x => x.Type == ClaimTypes.Role)
                .Select(x => x.Value)
                .ToArray();
            return role;
        }

        private async Task<List<Claim>> GetUserClaimsAsync(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            if (!string.IsNullOrWhiteSpace(user.Email))
                claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));

            if (!string.IsNullOrWhiteSpace(user.PhoneNumber))
                claims.Add(new Claim(ClaimTypes.MobilePhone, user.PhoneNumber));

            if (!string.IsNullOrWhiteSpace(user.Id))
                claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));

            if (!string.IsNullOrWhiteSpace(user.FullName))
                claims.Add(new Claim(JwtRegisteredClaimNames.GivenName, user.FullName));

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var item in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, item));
            }

            return claims;
        }

        private JwtSecurityToken ReadTokenData(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenData = tokenHandler.ReadJwtToken(token);

            return tokenData;

        }
    }
}
