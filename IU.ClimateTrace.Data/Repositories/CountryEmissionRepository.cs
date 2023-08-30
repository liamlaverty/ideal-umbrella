using IU.ClimateTrace.Data.Context;
using IU.ClimateTrace.Data.Models.ClimateTraceDbModels;
using IU.ClimateTrace.Data.Repositories.Interface;

namespace IU.ClimateTrace.Data.Repositories
{
    public class CountryEmissionRepository : IRepository<CountryEmission>
    {
        private IPostgresDataSource dataSource;


        public CountryEmissionRepository(IPostgresDataSource pgDataSource)
        {
            dataSource = pgDataSource;
        }

        public Task<CountryEmission> AddAsync(CountryEmission entity)
        {
            throw new NotImplementedException();
        }

        public Task<CountryEmission> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CountryEmission>> GetAllAsync()
        {
            try
            {
                using (var conn = await dataSource.GetDataSourceAsync())
                {
                    await using var command = conn.CreateCommand("SELECT * FROM country_emissions LIMIT 10");
                    await using var reader = await command.ExecuteReaderAsync();

                    List<CountryEmission> result = new List<CountryEmission>();

                    while (await reader.ReadAsync())
                    {
                        Console.WriteLine(reader.GetInt32(0));
                        var readEntity = new CountryEmission(
                            id: reader.GetInt32(0),
                            iso3_country: reader.IsDBNull(1) ? "" : reader.GetString(1),
                            start_time:  reader.GetDateTime(2),
                            end_time:  reader.GetDateTime(3),
                            original_inventory_sector: reader.IsDBNull(4) ? "" : reader.GetString(4),
                            gas: reader.IsDBNull(5) ? "" : reader.GetString(5),
                            emissions_quantity: reader.GetInt64(6),
                            emissions_quantity_units: reader.IsDBNull(7) ? "" : reader.GetString(7),
                            temporal_granularity: reader.IsDBNull(8) ? "" : reader.GetString(8),
                            origin_source: reader.IsDBNull(9) ? "" : reader.GetString(9),
                            source_created_date: reader.IsDBNull(10) ? null : reader.GetDateTime(10),
                            source_modified_date: reader.IsDBNull(11) ? null : reader.GetDateTime(11),
                            created_date: reader.GetDateTime(12),
                            modified_date: reader.GetDateTime(13)
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

        public Task<CountryEmission> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<CountryEmission> Update(CountryEmission entity)
        {
            throw new NotImplementedException();
        }
    }
}
