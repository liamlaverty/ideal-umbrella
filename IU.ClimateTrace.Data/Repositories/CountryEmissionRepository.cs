using IU.ClimateTrace.Common.DataFilters;
using IU.ClimateTrace.Data.Models.ClimateTraceDbModels;
using IU.ClimateTrace.Data.Repositories.Interface;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace IU.ClimateTrace.Data.Repositories
{

    public class CountryEmissionRepository : ICountryEmissionRepository
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

        public async Task<PagedResult<CountryEmission>> GetPagedAsync(
            string isoCountryCode,
            string gasName,
            DateTime startDate,
            DateTime endDate,
            OrderByDirection orderByDirection,
            CountryEmissionsOrderByCol orderByColumn,
            int pageNumber = 0,
            int pageSize = 50)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedResult<CountryEmission>> GetPagedAsync(int pageNumber = 0, int pageSize = 1000)
        {
            if (pageNumber < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(pageNumber), "must be 0 or greater");
            }
            if (pageSize < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(pageSize), "must be 1 or greater");
            }
            try
            {
                await connection.OpenAsync();
                await using var countCommand = new NpgsqlCommand()
                {
                    Connection = connection,
                    CommandText =
                    @"SELECT COUNT(*) FROM country_emissions",
                };
                await countCommand.PrepareAsync();
                var dbCount = await countCommand.ExecuteScalarAsync();
                Int64.TryParse(dbCount?.ToString(), out long countResult);

                // https://stackoverflow.com/a/17974/1663868
                var pageCount = (countResult + pageSize - 1) / pageSize;



                await using var command = new NpgsqlCommand()
                {
                    Connection = connection,
                    CommandText =
                    @"SELECT * FROM country_emissions 
                            LIMIT $1
                            OFFSET $2",
                    Parameters =
                    {
                        new() {Value = pageSize },
                        new() {Value = pageNumber * pageSize },
                    }
                };
                await command.PrepareAsync();
                await using var reader = await command.ExecuteReaderAsync();
                List<CountryEmission> results = new();
                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        var castResult = CountryEmissionFromDataReader(reader);
                        results.Add(castResult);
                    }
                }

                return new PagedResult<CountryEmission>(results, pageNumber, pageSize, pageCount: pageCount, totalRecords: countResult);
            }
            catch (Exception ex)
            {
                logger.LogError(new EventId(), ex, "Error Getting paged CountryEmission");
                throw;
            }
            finally
            {
                await connection.CloseAsync();
            }

        }

        /// <summary>
        ///  Deletes all entities form a database with a given ID
        /// </summary>
        /// <param name="id">the ID of the country emission to be deleted</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task DeleteAsync(int id)
        {
            try
            {
                await connection.OpenAsync();
                await using var command = new NpgsqlCommand()
                {
                    Connection = connection,
                    CommandText =
                    @"DELETE FROM country_emissions 
                            WHERE id = $1",
                    Parameters =
                    {
                        new() {Value = id },
                    }
                };
                await command.PrepareAsync();
                await command.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(new EventId(), ex, $"Error deleting country emission by ID {id}");
                throw;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        /// <summary>
        ///  Gets a single CountryEmisison by its ID
        ///  
        /// if no CountryEmission can be found, returns null
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// a country emission with the given ID
        /// 
        /// OR 
        /// 
        /// null
        /// </returns>
        public async Task<CountryEmission> GetAsync(int id)
        {
            try
            {
                await connection.OpenAsync();
                await using var command = new NpgsqlCommand()
                {
                    Connection = connection,
                    CommandText =
                    @"SELECT * FROM country_emissions
                                WHERE id = $1 LIMIT 1",
                    Parameters =
                    {
                        new() {Value = id },
                    }
                };
                await command.PrepareAsync();
                await using var reader = await command.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        var castResult = CountryEmissionFromDataReader(reader);
                        return castResult;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                logger.LogError(new EventId(), ex, "Error getting CountryEmission");
                throw;
            }
            finally
            {
                await connection.CloseAsync();
            }
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
