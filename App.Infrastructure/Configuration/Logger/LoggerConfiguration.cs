using Microsoft.Extensions.Logging;
using System.Reflection;

namespace App.Infrastructure.Configuration
{
    public static class LoggerConfiguration
    {
        public static ILoggerFactory ConfigureLoggerFactory(this ILoggerFactory log)
        {
            var projectName = Assembly.GetEntryAssembly().GetName().Name.Split(".")[0].ToLower();

            var name = string.Format("Logs/{0}", projectName);
            log.AddFile(name + "-{Date}.txt", LogLevel.Information);

            return log;
        }
    }
}
