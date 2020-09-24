using App.API.Controllers.Shared;
using App.API.Utils.Extensions;
using App.Domain.Models.Users;
using App.Infrastructure.Authorization.Enums;
using App.Infrastructure.Authorization.Interfaces;
using App.Infrastructure.Authorization.Models.Login;
using App.Infrastructure.Authorization.Models.Registration;
using App.Infrastructure.Authorization.Models.Response;
using App.Infrastructure.Utils.StaticContent;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NETCore.MailKit.Core;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace App.API.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IAuthService _authService;
        private readonly IEmailService _emailService;
        private readonly UserManager<User> _userManager;

        public AccountController(IAuthService authService,
                                 IEmailService emailService,
                                 UserManager<User> userManager)
        {
            _authService = authService;
            _emailService = emailService;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync(Login model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.GetErrors();
                    return UnprocessableEntity(errors);
                }

                var result = await _authService.LoginAsync(model);

                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());

                return BadRequest();
            }
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdminAsync(Register model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.GetErrors();
                }

                var result = await _authService.RegisterAsync(model, UserRole.Admin);

                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());

                return BadRequest();
            }
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.User)]
        [Route("register-user")]
        public async Task<IActionResult> RegisterUserAsync(Register model, [Required] string returnUrl)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.GetErrors();
                    return UnprocessableEntity(errors);
                }

                var result = await _authService.RegisterAsync(model, UserRole.User);
                await SendEmailAsync(model.Email, result, returnUrl);

                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());

                return BadRequest();
            }
        }

        [HttpGet]
        [Route("verifyemail")]
        public async Task<IActionResult> VerifyEmail(string userId, string code, string returnUrl)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return Redirect(returnUrl + "?succeeded=false");
            }

            var confirmationResult = await _userManager.ConfirmEmailAsync(user, code);
            if (confirmationResult.Succeeded)
            {
                return Redirect(returnUrl + "?succeeded=true");
            }

            return Redirect(returnUrl + "?succeeded=false");
        }

        private async Task SendEmailAsync(string email, AuthResponse result, string returnUrl)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                var url = Url.Action("verifyemail",
                                     "Account",
                                     new { userId = user.Id, code, returnUrl },
                                     Request.Scheme,
                                     Request.Host.ToString());

                var link = $"<a class='btn-primary' href='{url}?token={result.TokenResponse.Token}'>Verify Your Account</a>";

                await _emailService.SendAsync(user.Email, "App - Email Verification", link, true);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());
                throw ex;
            }
        }
    }
}
