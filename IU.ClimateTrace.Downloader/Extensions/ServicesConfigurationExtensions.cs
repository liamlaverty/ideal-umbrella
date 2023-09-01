using IU.ClimateTrace.Common.Config;
using IU.ClimateTrace.Downloader.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IU.ClimateTrace.Downloader.Extensions
{
    public static class ServicesConfigurationExtensions
    {
        public static void AddClimateTraceDownloaderServices(
            this IServiceCollection services)
        {
            var configBuilder = new ConfigurationBuilder()
                         .SetBasePath(Directory.GetCurrentDirectory())
                         .AddJsonFile("appsettings.json", optional: false);

            IConfiguration _config = configBuilder.Build();

            // next line requires `Microsoft.Extensions.Configuration.Binder`
            // to avoid the error: 'IConfiguration' does not contain a definition for 'Get'
            var appConfig = _config.GetSection(ClimateTraceDownloaderSettings.ConfigName).Get<ClimateTraceDownloaderSettings>() ??
                    throw new ApplicationException($"appConfig was null, verify appsettings.json contains a section for '{ClimateTraceDownloaderSettings.ConfigName}'");


            services.AddHttpClient("fileDownloaderServiceClient", client =>
            {
                client.Timeout = TimeSpan.FromMinutes(15);
                client.BaseAddress = new Uri(appConfig.DownloadConfiguration.ClimateTraceBaseUrl);
            });

            services.AddScoped<IFileDownloaderService, FileDownloaderService>();
            services.AddScoped<IFileUnzipperService, FileUnzipperService>();
            services.AddScoped<IClimateTraceDownloader, ClimateTraceDownloader>();

            // next line requires `Microsoft.Extensions.Options.ConfigurationExtensions`
            // to avoid the error:
            // Error CS1503 cannot convert from Microsoft.Extensions.Configuration.IConfigurationSection to System.Action<>
            services.Configure<ClimateTraceDownloaderSettings>(
                _config.GetSection(
                    ClimateTraceDownloaderSettings.ConfigName
                    )
            );
        }
    }
}
