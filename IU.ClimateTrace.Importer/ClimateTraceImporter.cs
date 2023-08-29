using IU.ClimateTrace.Common.Config;
using IU.ClimateTrace.Data.Models.ClimateTraceDbModels;
using Microsoft.Extensions.Options;
using NetTopologySuite.Geometries;
using Npgsql;

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
            //var countryResult = await GetCountryEmissions();

            //foreach (var item in countryResult)
            //{
            //    Console.WriteLine(JsonConvert.SerializeObject(item));
            //}

            var assetResult = await GetAssetEmissions();
            foreach (var item in assetResult)
            {
                Console.WriteLine($"{item.AssetName} - StAstext is null? {(item.StAstext == null ? "true" : "false")}" );
            }
        }


        /// <summary>
        /// Dev/test method to get asset emissions data
        /// </summary>
        /// <returns></returns>
        private async Task<IEnumerable<AssetEmission>> GetAssetEmissions()
        {
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(_settings.ImportConfiguration.PostgresDbConnection);
            dataSourceBuilder.UseNetTopologySuite();
            await using NpgsqlDataSource dataSource = dataSourceBuilder.Build();


            await using var command = dataSource.CreateCommand("SELECT * FROM asset_emissions LIMIT 5000");
            await using var reader = await command.ExecuteReaderAsync();

            List<AssetEmission> result = new List<AssetEmission>();

            while (await reader.ReadAsync())
            {
                Console.WriteLine(reader.GetInt32(0));
                var readEntity = new AssetEmission(
                    asset_id: reader.GetInt32(0),
                    iso3_country: reader.GetString(1),
                    original_inventory_sector: reader.GetString(2),
                    start_time: reader.GetDateTime(3),
                    end_time: reader.GetDateTime(4),
                    temporal_granularity: reader.GetString(5),
                    gas: reader.GetString(6),
                    emissions_quantity: reader.GetInt64(7),
                    emissions_factor: reader.GetDecimal(8),
                    emissions_factor_units: reader.GetString(9),
                    capacity: reader.IsDBNull(10) ? 0 : reader.GetDecimal(10),
                    capacity_units: reader.IsDBNull(12) ? "" : reader.GetString(11),
                    capacity_factor: reader.IsDBNull(12) ? 0 : reader.GetDecimal(12),
                    activity: reader.IsDBNull(13) ? 0 : reader.GetDecimal(13),
                    activity_units: reader.IsDBNull(13) ? "" : reader.GetString(14),
                    origin_source: reader.GetString(15),
                    source_created_date: reader.IsDBNull(16) ? null : reader.GetDateTime(16),
                    source_modified_date: reader.IsDBNull(17) ? null : reader.GetDateTime(17),
                    created_date: reader.GetDateTime(18),
                    modified_date: reader.IsDBNull(19) ? null : reader.GetDateTime(19),
                    asset_name: reader.GetString(20),
                    asset_type: reader.GetString(21),
                    // st_astext: null
                    st_astext: reader.GetFieldValue<Geometry>(22)
                );
              
                result.Add(readEntity);
            }
            return result;
        }


        /// <summary>
        ///  Dev/test method to check the database can be connected to
        /// </summary>
        /// <returns></returns>
        private async Task<IEnumerable<CountryEmission>> GetCountryEmissions()
        {
            var conn = new NpgsqlConnection(
                connectionString:
                _settings.ImportConfiguration.PostgresDbConnection
                );
            conn.Open();
            using var cmd = new NpgsqlCommand();
            cmd.Connection = conn;

            return null; 
            // return await conn.QueryAsync<CountryEmission>(
            //    "SELECT * FROM country_emissions LIMIT 50");
        }
    }



}