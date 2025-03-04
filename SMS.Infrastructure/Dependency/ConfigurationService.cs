using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace SMS.Infrastructure.Dependency
{
    public static class ConfigurationService
    {
        public static WebApplicationBuilder AddConfigurations(this WebApplicationBuilder builder)
        {
            var environment = builder.Environment;

            var configuration = builder.Configuration;

            Console.WriteLine($"Environment: {environment.EnvironmentName}.");

            configuration
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true, reloadOnChange: true);
            return builder;
        }
    }
}
