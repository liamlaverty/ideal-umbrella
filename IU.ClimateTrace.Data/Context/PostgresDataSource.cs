using IU.ClimateTrace.Common.Config;
using Microsoft.Extensions.Options;
using Npgsql;

namespace IU.ClimateTrace.Data.Context
{
    public class PostgresDataSource : IPostgresDataSource
    {
        private readonly ClimateTraceDownloaderSettings _settings;
        private readonly NpgsqlDataSource dataSource;

        public PostgresDataSource(IOptions<ClimateTraceDownloaderSettings> settings)
        {
            _settings = settings.Value;
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(_settings.ImportConfiguration.PostgresDbConnection);
            dataSourceBuilder.UseNetTopologySuite();
            dataSource = dataSourceBuilder.Build();
        }

        public Task<NpgsqlDataSource> GetDataSourceAsync()
        {
            return Task.FromResult(dataSource);
        }
    }
}
