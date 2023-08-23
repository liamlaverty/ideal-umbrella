using IU.ClimateTrace.Downloader.Models.Config;
using IU.ClimateTrace.Downloader.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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


            ServiceProvider serviceProvider = new ServiceCollection()
                //.AddHttpClient<FileDownloaderService>(
                //    options =>
                //    {
                //        options.BaseAddress = new Uri("");
                //    }
                //)
                .Configure<ClimateTraceDownloaderSettings>(
                    _config.GetSection(ClimateTraceDownloaderSettings.ConfigName)
                    )
                .AddScoped<IClimateTraceDownloader, ClimateTraceDownloader>()
                .AddScoped<IFileDownloaderService, FileDownloaderService>()
                .AddScoped<IFileUnzipperService, FileUnzipperService>()
                
                .BuildServiceProvider();

            downloader = serviceProvider.GetRequiredService<IClimateTraceDownloader>();

        }
    }
}