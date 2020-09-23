using App.Domain.Models.Users;
using App.Domain.Utils.Settings;
using App.Infrastructure.Authorization.Enums;
using App.Infrastructure.Authorization.Interfaces;
using App.Infrastructure.Authorization.Models.Login;
using App.Infrastructure.Authorization.Models.Registration;
using App.Infrastructure.Authorization.Models.Response;
using App.Infrastructure.Authorization.Utils.Transformers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace App.Infrastructure.Authorization.Services
{
    public class AuthService : IAuthService
    {
        private readonly IBearerTokenService _bearerTokenService;
        //private readonly IExternalLoginService _externalLoginService;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<AuthService> _logger;

        public AuthService
        (
            IBearerTokenService bearerTokenService,
            //IExternalLoginService externalLoginService,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<AuthService> logger
        )
        {
            _bearerTokenService = bearerTokenService;
            //_externalLoginService = externalLoginService;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        public async Task<AuthResponse> ChangePasswordAsync(ChangePassword model)
        {
            var result = new AuthResponse();
            var user = await _userManager.FindByEmailAsync(model.Email);
            IdentityResult changePasswordResult = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

            if (!changePasswordResult.Succeeded)
            {
                result.Errors = GetErrors(changePasswordResult);

                return result;
            }

            result.Succeded = true;

            return result;
        }

        public async Task<string> GetForgotPasswordTokenAsync(ForgotPassword model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            return token;
        }

        public async Task<AuthResponse> GetRefreshTokenAsync(string refreshToken)
        {
            var result = new AuthResponse();

            var tokenIsValid = _bearerTokenService.ValidateToken(refreshToken, true);

            if (tokenIsValid)
            {
                var userId = _bearerTokenService.GetUserId(refreshToken);
                var user = await _userManager.FindByIdAsync(userId);

                if (user != null)
                {
                    var token = await GetJwtTokenAsync(user);
                    result.TokenResponse = token;
                    result.Succeded = true;

                    return result;
                }
            }
            return result;
        }

        public async Task<UserSettings> GetUserClaimsAsync(string token)
        {
            var userId = _bearerTokenService.GetUserId(token);
            var roles = _bearerTokenService.GetUserRoles(token);

            var user = await _userManager.FindByIdAsync(userId);
            var username = user.FullName;

            var result = new UserSettings()
            {
                UserId = userId,
                UserRoles = roles,
                UserName = username
            };

            return result;
        }

        public async Task<AuthResponse> LoginAsync(Login model)
        {
            var signInResult = await _signInManager.PasswordSignInAsync(model.Email, model.Password,
                    isPersistent: false, lockoutOnFailure: true);
            var result = new AuthResponse();

            if (!signInResult.Succeeded)
            {
                result.Succeded = false;

                // Generate error messages
            }
            else
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                result.TokenResponse = await GetJwtTokenAsync(user);
            }

            return result;
        }

        public async Task<AuthResponse> RegisterAsync(Register model, UserRole role)
        {

            var result = new AuthResponse();

            var user = RegistrationTransformer.Transform(model);

            var registrationResponse = await _userManager.CreateAsync(user, model.Password);

            if (!registrationResponse.Succeeded)
            {
                result.Succeded = false;
                result.Errors = GetErrors(registrationResponse);
                _logger.LogError("Registration FAILED: " + registrationResponse.ToString());

                return result;
            }

            var roleName = Enum.GetName(typeof(UserRole), role).ToLower();
            var roleAssignmentResult = await _userManager.AddToRoleAsync(user, roleName);

            if (!roleAssignmentResult.Succeeded)
            {
                await _userManager.DeleteAsync(user);
                result.Errors = GetErrors(roleAssignmentResult);
                _logger.LogError("Role assignment FAILED: " + roleAssignmentResult.ToString());
                return result;
            }

            result.Succeded = true;
            result.TokenResponse = await GetJwtTokenAsync(user);

            return result;
        }

        private async Task<TokenResponse> GetJwtTokenAsync(User user)
        {
            var responseToken = await _bearerTokenService.GenerateJwtTokenAsync(user);
            var tokenString = new JwtSecurityTokenHandler().WriteToken(responseToken);

            var refreshToken = await _bearerTokenService.GenerateJwtTokenAsync(user, true);
            var refreshTokenString = new JwtSecurityTokenHandler().WriteToken(refreshToken);

            var tokenResponse = new TokenResponse
            {
                Token = tokenString,
                RefreshToken = refreshTokenString,
            };

            return tokenResponse;
        }

        private List<AuthErrorResponse> GetErrors(IdentityResult model)
        {
            var result = new List<AuthErrorResponse>();

            foreach (var error in model.Errors)
            {
                result.Add(new AuthErrorResponse()
                {
                    Key = error.Code,
                    Value = error.Description
                });
            }

            return result;
        }
    }
}
