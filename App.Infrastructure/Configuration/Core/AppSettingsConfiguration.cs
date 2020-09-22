using App.Domain.Utils.Settings;
using Microsoft.Extensions.Configuration;

namespace App.Infrastructure.Configuration.Core
{
    public static class AppSettingsConfiguration
    {
        public static IConfiguration ConfigureCoreSettings(this IConfiguration configuration)
        {

            AppSettings.DisplayUrl = configuration.GetValue<string>("AppSettings:DispalyUrl");
            AppSettings.UploadPath = configuration.GetValue<string>("AppSettings:UploadPath");
            return configuration;
        }
    }
}
