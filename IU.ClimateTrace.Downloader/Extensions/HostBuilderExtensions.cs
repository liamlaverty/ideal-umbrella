using Microsoft.Extensions.Hosting;

namespace IU.ClimateTrace.Downloader.Extensions
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder AddClimateTraceDownloader(
            this IHostBuilder builder)
        {
            builder.ConfigureServices((hostContext, services) =>
            {
                services.AddClimateTraceDownloaderServices();
            });

            return builder;

        }
    }
}
