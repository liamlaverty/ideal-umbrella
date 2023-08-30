using IU.ClimateTrace.Data.Models.ClimateTraceDbModels;
using IU.ClimateTrace.Data.Repositories;
using IU.ClimateTrace.Data.Repositories.Interface;
using Microsoft.Extensions.DependencyInjection;
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

                services.AddScoped<IRepository<AssetEmission>, AssetEmissionRepository>();
                services.AddScoped<IRepository<CountryEmission>, CountryEmissionRepository>();

            });
            return builder;
        }
    }
}
