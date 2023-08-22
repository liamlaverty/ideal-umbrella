using Microsoft.Extensions.DependencyInjection;

namespace IU.ClimateTrace.Downloader
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddScoped<IClimateTraceDownloader, ClimateTraceDownloader>()
                .BuildServiceProvider();

            var dataDownloader = serviceProvider.GetRequiredService<IClimateTraceDownloader>();

            Console.WriteLine($"Starting downloader. Result:");
            Console.WriteLine(dataDownloader.DownloadData());


            Console.WriteLine("Complete, press any key to exit");
            Console.Read();
        }
    }
}