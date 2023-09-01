using IU.ClimateTrace.Data.Models.ClimateTraceDbModels;
using IU.ClimateTrace.Data.Repositories.Interface;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace IU.ClimateTrace.Data.Repositories
{
    public class CountryEmissionRepository : IRepository<CountryEmission>
    {
        private readonly ILogger logger;
        private readonly NpgsqlConnection connection;


        public CountryEmissionRepository(NpgsqlConnection connection,
            ILogger<CountryEmissionRepository> logger)
        {
            this.logger = logger;
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
                await command.PrepareAsync();
                await command.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(new EventId(), ex, "Error inserting CountryEmission");

                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        public async Task<PagedResult<CountryEmission>> GetPagedAsync(int pageNumber = 0, int pageSize = 1000)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }


        public async Task<CountryEmission> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<CountryEmission> Update(CountryEmission entity)
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


        /// <summary>
        /// Checks if an country emission entity in the database
        /// 
        /// </summary>
        /// <param name="entity">A country emission object to check</param>
        /// <returns>
        /// true if the entity already exists
        /// false if not
        /// </returns>
        public async Task<bool> Exists(CountryEmission entity)
        {
            // PKEY: iso3_country, start_time, gas, temporal_granularity, original_inventory_sector
            try
            {
                await connection.OpenAsync();
                await using var command = new NpgsqlCommand()
                {
                    Connection = connection,
                    CommandText =
                    @"SELECT EXISTS 
                        (
                            SELECT 1 FROM country_emissions
                                WHERE iso3_country = $1
                                AND start_time = $2
                                AND gas = $3
                                AND temporal_granularity = $4
                                AND original_inventory_sector = $5
                        )",
                    Parameters =
                    {
                        new() { Value = entity.Iso3Country },
                        new() { Value = entity.StartTime },
                        new() { Value = entity.Gas },
                        new() { Value = entity.TemporalGranularity },
                        new() { Value=entity.OriginalInventorySector },
                    }
                };
                await command.PrepareAsync();
                var result = await command.ExecuteScalarAsync();
                Boolean.TryParse(result?.ToString(), out bool didExist);
                return didExist;
            }
            catch (Exception ex)
            {
                logger.LogError(new EventId(), ex, "Error inserting AssetEmission");
                throw;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }
    }
}
