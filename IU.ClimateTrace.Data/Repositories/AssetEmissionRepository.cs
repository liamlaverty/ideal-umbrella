using IU.ClimateTrace.Data.Context;
using IU.ClimateTrace.Data.Models.ClimateTraceDbModels;
using IU.ClimateTrace.Data.Repositories.Interface;
using NetTopologySuite.Geometries;

namespace IU.ClimateTrace.Data.Repositories
{

    public class AssetEmissionRepository : IAssetEmissionRepository
    {
        private IPostgresContext context;

        public AssetEmissionRepository(IPostgresContext pgContext)
        { 
            this.context = pgContext;
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
                using (var conn = await context.GetDataSourceAsync())
                {
                    await using var command = conn.CreateCommand("SELECT * FROM asset_emissions LIMIT 500");
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
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<AssetEmission> Update(AssetEmission entity)
        {
            throw new NotImplementedException();
        }
    }
}
