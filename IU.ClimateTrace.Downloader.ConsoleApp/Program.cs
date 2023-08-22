using IU.ClimateTrace.Downloader.Models.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IU.ClimateTrace.Downloader
{
    class Program
    {
        static void Main(string[] args)
        {
            var startup = new Startup();

            Console.WriteLine($"Starting downloader. Result:");
            Console.WriteLine(startup.downloader.DownloadData());


            Console.WriteLine("Complete, press any key to exit");
            Console.Read();
        }
    }

    public class Startup
    {
        public IClimateTraceDownloader downloader { get; private set; }

        public Startup()
        {
            var configBuilder = new ConfigurationBuilder()
                          .SetBasePath(Directory.GetCurrentDirectory())
                          .AddJsonFile("appsettings.json", optional: false);
            IConfiguration _config = configBuilder.Build();

            var serviceProvider = new ServiceCollection()
                .Configure<ClimateTraceDownloaderSettings>(
                    _config.GetSection(ClimateTraceDownloaderSettings.ConfigName)
                    )
                .AddScoped<IClimateTraceDownloader, ClimateTraceDownloader>()
                .BuildServiceProvider();

            downloader = serviceProvider.GetRequiredService<IClimateTraceDownloader>();
        }
    }
}