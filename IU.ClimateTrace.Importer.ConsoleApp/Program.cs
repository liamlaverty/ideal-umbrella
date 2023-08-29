using IU.ClimateTrace.Common.Config;
using IU.ClimateTrace.Importer.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IU.ClimateTrace.Importer.ConsoleApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            var startup = new Startup();

            await startup.importer.ImportData();
            Console.WriteLine("Complete, press any key to exit");

        }
    }

    public class Startup
    {
        public IClimateTraceImporter importer { get; private set; }

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
                .AddClimateTraceImporter();

            var host = builder.Build();

            importer = host.Services.GetRequiredService<IClimateTraceImporter>();

        }
    }
}