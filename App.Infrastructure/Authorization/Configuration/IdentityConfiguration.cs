using App.Infrastructure.Authorization.Interfaces;
using App.Infrastructure.Authorization.Models.Registration;
using App.Infrastructure.Authorization.Services;
using App.Infrastructure.Authorization.Validations;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace App.Infrastructure.Authorization.Configuration
{
    public static class Identity
    {
        public static IServiceCollection ConfigureIdentity(this IServiceCollection services)
        {
            services.Configure<IdentityOptions>(options =>
            {
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                options.User.RequireUniqueEmail = false;

                options.SignIn.RequireConfirmedEmail = false;
                //options.SignIn.RequireConfirmedEmail = true; unncoment after providing smtp creds
                options.SignIn.RequireConfirmedPhoneNumber = false;

                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 0;
            });

            services.AddTransient<IValidator<Register>, RegistrationValidator>();
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}
