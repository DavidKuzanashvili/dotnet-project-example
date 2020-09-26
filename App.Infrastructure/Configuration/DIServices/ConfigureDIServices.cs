using App.Domain.Interfaces.Languages;
using App.Domain.Interfaces.Users;
using App.Infrastructure.Services.Languages;
using App.Infrastructure.Services.Users;
using Microsoft.Extensions.DependencyInjection;

namespace App.Infrastructure.Configuration.DIServices
{
    public static class ConfigureDIServices
    {
        public static IServiceCollection AddAppDIServices(this IServiceCollection services)
        {
            services.AddFluentValidationDIServices();
            services.AddScoped<ILanguageService, LangugeService>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
