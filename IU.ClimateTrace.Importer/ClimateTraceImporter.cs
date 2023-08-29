using Dapper;
using IU.ClimateTrace.Common.Config;
using IU.ClimateTrace.Data.Models.ClimateTraceDbModels;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Npgsql;
using System.Net.Http.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace IU.ClimateTrace.Importer
{
    public interface IClimateTraceImporter
    {
        Task ImportData();
    }
    public class ClimateTraceImporter : IClimateTraceImporter
    {
        private readonly ClimateTraceDownloaderSettings _settings;

        public ClimateTraceImporter(IOptions<ClimateTraceDownloaderSettings> climateTraceImporterConfig) 
        {
            Console.WriteLine($"starting {nameof(ClimateTraceImporter)}");
            _settings = climateTraceImporterConfig.Value;
        }

        public async Task ImportData()
        {
            var result = await GetCountryEmissions();

            foreach (var item in result)
            {
                Console.WriteLine(JsonConvert.SerializeObject(item));
            }
        }


        private async Task<IEnumerable<CountryEmission>> GetCountryEmissions()
        {
            var conn = new NpgsqlConnection(
                connectionString:
                _settings.ImportConfiguration.PostgresDbConnection
                );
            conn.Open();
            using var cmd = new NpgsqlCommand();
            cmd.Connection = conn;

            return await conn.QueryAsync<CountryEmission>(
                "SELECT * FROM country_emissions LIMIT 50");
        }
    }



}