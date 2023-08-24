using IU.ClimateTrace.Downloader.Models.Config;
using IU.ClimateTrace.Downloader.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using IU.ClimateTrace.Downloader.Extensions;

namespace IU.ClimateTrace.Downloader
{
    public class Startup
    {
        public IClimateTraceDownloader downloader { get; private set; }

        public Startup()
        {
            var configBuilder = new ConfigurationBuilder()
                          .SetBasePath(Directory.GetCurrentDirectory())
                          .AddJsonFile("appsettings.json", optional: false);
            IConfiguration _config = configBuilder.Build();
            var appConfig = _config.GetSection(ClimateTraceDownloaderSettings.ConfigName).Get<ClimateTraceDownloaderSettings>();
            if (appConfig == null)
            {
                throw new InvalidOperationException(
                    $"{nameof(appConfig)} is null after it should be set. Check appsettings.json contains well formatted settings");
            }

            var builder = new HostBuilder()
                .AddClimateTraceDownloader();

            var host = builder.Build();

            downloader = host.Services.GetRequiredService<IClimateTraceDownloader>();
        }
    }
}