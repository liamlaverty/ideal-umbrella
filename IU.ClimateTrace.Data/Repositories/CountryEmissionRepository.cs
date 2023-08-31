using IU.ClimateTrace.Data.Models.ClimateTraceDbModels;
using IU.ClimateTrace.Data.Repositories.Interface;
using Npgsql;

namespace IU.ClimateTrace.Data.Repositories
{
    public class CountryEmissionRepository : IRepository<CountryEmission>
    {
        private readonly NpgsqlConnection connection;


        public CountryEmissionRepository(NpgsqlConnection connection)
        {
            this.connection = connection;
        }

        public async Task AddAsync(CountryEmission entity)
        {
            DateTime now = DateTime.UtcNow;
            try
            {
                await connection.OpenAsync();
                await using var command = new NpgsqlCommand()
                {
                    Connection = connection,
                    CommandText = @"
                        INSERT INTO country_emissions(
                        iso3_country, 
                        start_time, 
                        end_time, 
                        original_inventory_sector, 
                        gas,
                        emissions_quantity, 
                        emissions_quantity_units, 
                        temporal_granularity,
                        origin_source, 
                        source_created_date, 
                        source_modified_date, 
                        created_date,
                        modified_date) 
                
                        VALUES(
                            $1, 
                            $2, 
                            $3, 
                            $4, 
                            $5, 
                            $6,
                            $7, 
                            $8, 
                            $9,
                            $10, 
                            $11, 
                            $12, 
                            $13
                        )
                        ON CONFLICT
                            ON CONSTRAINT country_emissions_pkey
                                DO NOTHING  
                        ",
                    Parameters =
                    {
                        new() { Value = entity.Iso3Country },
                        new() { Value = entity.StartTime },
                        new() { Value = entity.EndTime },
                        new() { Value = entity.OriginalInventorySector },
                        new() { Value = entity.Gas },
                        new() { Value = entity.EmissionsQuantity },
                        new() { Value = entity.EmissionsQuantityUnits },
                        new() { Value = entity.TemporalGranularity },
                        new() { Value = entity.OriginSource },
                        new() { Value = entity.SourceCreatedDate },
                        new() { Value = (entity.SourceModifiedDate == null ? DBNull.Value : entity.SourceModifiedDate) },
                        new() { Value = now },
                        new() { Value = now },
                    }
                };

                await command.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        public Task<CountryEmission> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CountryEmission>> GetAllAsync()
        {
            try
            {
                List<CountryEmission> result = new List<CountryEmission>();
                await connection.OpenAsync();

                await using var command = new NpgsqlCommand("SELECT * FROM country_emissions LIMIT 100", connection);
                await using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    result.Add(
                         CountryEmissionFromDataReader(reader));
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




        public Task<CountryEmission> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<CountryEmission> Update(CountryEmission entity)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Maps a DataReader result into an <seealso cref="CountryEmission"/> object
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private static CountryEmission CountryEmissionFromDataReader(NpgsqlDataReader reader)
        {
            return new CountryEmission(
                id: reader.GetInt32(0),
                iso3_country: reader.IsDBNull(1) ? "" : reader.GetString(1),
                start_time: reader.GetDateTime(2),
                end_time: reader.GetDateTime(3),
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
        }
    }
}
