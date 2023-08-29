using Microsoft.Extensions.Hosting;

namespace IU.ClimateTrace.Importer.Extensions
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder AddClimateTraceImporter(
            this IHostBuilder builder)
        {
            builder.ConfigureServices((hostContext, services) =>
            {
                services.AddClimateTraceImporterServices();
                
            });
            return builder;
        }
    }
}
