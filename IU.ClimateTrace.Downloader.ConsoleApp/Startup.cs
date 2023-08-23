using IU.ClimateTrace.Downloader.Models.Config;
using IU.ClimateTrace.Downloader.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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

            var builder = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHttpClient();
                    services.AddScoped<IClimateTraceDownloader, ClimateTraceDownloader>();
                    services.AddScoped<IFileDownloaderService, FileDownloaderService>();
                    services.AddScoped<IFileUnzipperService, FileUnzipperService>();
                    services.Configure<ClimateTraceDownloaderSettings>(
                        _config.GetSection(ClimateTraceDownloaderSettings.ConfigName)
                    );
                });
            var host = builder.Build();

            downloader = host.Services.GetRequiredService<IClimateTraceDownloader>();

            //ServiceProvider serviceProvider = new ServiceCollection()
            //    //.AddHttpClient<FileDownloaderService>(
            //    //    options =>
            //    //    {
            //    //        options.BaseAddress = new Uri("");
            //    //    }
            //    //)
            //    .Configure<ClimateTraceDownloaderSettings>(
            //        _config.GetSection(ClimateTraceDownloaderSettings.ConfigName)
            //        )
            //    .AddScoped<IClimateTraceDownloader, ClimateTraceDownloader>()
            //    .AddScoped<IFileDownloaderService, FileDownloaderService>()
            //    .AddScoped<IFileUnzipperService, FileUnzipperService>()

            //    .BuildServiceProvider();

            //downloader = serviceProvider.GetRequiredService<IClimateTraceDownloader>();

        }
    }
}