using IU.ClimateTrace.Common.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IU.ClimateTrace.Importer.Extensions
{
    public static class ServicesConfigurationExtensions
    {
        public static void AddClimateTraceImporterServices(
            this IServiceCollection services)
        {
            // next line requires `Microsoft.Extensions.Configuration.FileExtensions`, then `Microsoft.Extensions.Configuration.Json`
            // to avoid the error: 'IConfiguration' does not contain a definition for 'SetBasePath'
            // and to avoid the error: 'IConfiguration' does not contain a definition for 'AddJsonFile'
            var configBuilder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: false);

            IConfiguration _config = configBuilder.Build();

            // next line requires `Microsoft.Extensions.Configuration.Binder`
            // to avoid the error: 'IConfiguration' does not contain a definition for 'Get'
            var appConfig = _config.GetSection(ClimateTraceDownloaderSettings.ConfigName).Get<ClimateTraceDownloaderSettings>() ??
                    throw new ApplicationException($"appConfig was null, verify appsettings.json contains a section for '{ClimateTraceDownloaderSettings.ConfigName}'");


            services.AddScoped<IClimateTraceImporter, ClimateTraceImporter>();

            // next line requires `Microsoft.Extensions.Options.ConfigurationExtensions`
            // to avoid the error:
            // Error CS1503 cannot convert from Microsoft.Extensions.Configuration.IConfigurationSection to System.Action<>
            services.Configure<ClimateTraceDownloaderSettings>(
                _config.GetSection(
                    ClimateTraceDownloaderSettings.ConfigName
                    ));
        }
    }
}
