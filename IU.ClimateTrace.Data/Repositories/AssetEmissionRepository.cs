using IU.ClimateTrace.Data.Models.ClimateTraceDbModels;
using IU.ClimateTrace.Data.Repositories.Interface;
using NetTopologySuite.Geometries;
using Npgsql;

namespace IU.ClimateTrace.Data.Repositories
{

    public class AssetEmissionRepository : IRepository<AssetEmission>
    {
        private readonly NpgsqlConnection connection;

        public AssetEmissionRepository(NpgsqlConnection connection)
        {
            this.connection = connection;
        }

        public async Task<AssetEmission> AddAsync(AssetEmission entity)
        {
            throw new NotImplementedException();
        }

        public async Task<AssetEmission> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<AssetEmission> GetAsync(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<AssetEmission>> GetAllAsync()
        {
            try
            {
                List<AssetEmission> result = new List<AssetEmission>();
                await connection.OpenAsync();

                await using var command = new NpgsqlCommand("SELECT * FROM asset_emissions LIMIT 100", connection);
                await using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    result.Add(AssetEmissionFromDataReader(reader));
                }
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }



        public Task<AssetEmission> Update(AssetEmission entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Maps a DataReader result into an <seealso cref="AssetEmission"/> object
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private static AssetEmission AssetEmissionFromDataReader(NpgsqlDataReader reader)
        {
            return new AssetEmission(
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
                st_astext: reader.GetFieldValue<Geometry>(22)
            );
        }
    }
}
