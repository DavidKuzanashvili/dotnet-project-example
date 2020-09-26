using Microsoft.Extensions.DependencyInjection;

namespace App.Infrastructure.Utils.Helpers.Uploaders
{
    public static class ConfigureUploaderDIServices
    {
        public static IServiceCollection AddUploadDIService(this IServiceCollection services)
        {
            services.AddScoped<IUploadService, UploadService>();

            return services;
        }
    }
}
