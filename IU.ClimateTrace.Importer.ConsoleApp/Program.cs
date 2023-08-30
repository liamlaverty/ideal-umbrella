using IU.ClimateTrace.Common.Config;
using IU.ClimateTrace.Importer.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;

namespace IU.ClimateTrace.Importer.ConsoleApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Starting app");

            var startup = new Startup();


            Console.WriteLine("Importing data");

            await startup.importer.ImportData();

            Console.WriteLine("Printing sample data");

            await startup.importer.PrintSampleData();

            
            Console.WriteLine("Complete, press any key to exit");

        }
    }

    public class Startup
    {
        public IClimateTraceImporter importer { get; private set; }

        public Startup()
        {
            var builder = new HostBuilder()
                .AddClimateTraceImporter();
            var host = builder.Build();
            importer = host.Services.GetRequiredService<IClimateTraceImporter>();
        }
    }
}