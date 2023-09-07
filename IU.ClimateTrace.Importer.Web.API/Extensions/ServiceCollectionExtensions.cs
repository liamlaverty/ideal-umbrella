
using IU.ClimateTrace.Data.Models.ClimateTraceDbModels;
using IU.ClimateTrace.Data.Repositories.Interface;
using IU.ClimateTrace.Data.Repositories;
using IU.ClimateTrace.Common.Config;
using Npgsql;

namespace IU.ClimateTrace.Importer.Web.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPostgresRepositories(this IServiceCollection services)
        {
            var configBuilder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: false)
               .AddJsonFile("appsettings.development.json", optional: true);


            IConfiguration _config = configBuilder.Build();
            var appConfig = _config.GetSection(ClimateTraceDownloaderSettings.ConfigName).Get<ClimateTraceDownloaderSettings>() ??
                    throw new ApplicationException($"appConfig was null, verify appsettings.json contains a section for '{ClimateTraceDownloaderSettings.ConfigName}'");

            var npgSqlLoggerFactory = LoggerFactory.Create(
                 builder => {
                     builder.AddConsole()
                        .AddFilter(level => level >= LogLevel.Warning);
                 });

            services.AddNpgsqlDataSource(
                appConfig.ImportConfiguration.PostgresDbConnection,
                builder => {
                    builder.UseNetTopologySuite();
                    builder.UseLoggerFactory(npgSqlLoggerFactory);
                });
            services.AddScoped<IRepository<AssetEmission>, AssetEmissionRepository>();
            services.AddScoped<ICountryEmissionRepository, CountryEmissionRepository>();
            return services;
        }
    }
}