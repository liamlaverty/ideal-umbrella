﻿using IU.ClimateTrace.Data.Models.ClimateTraceDbModels;
using IU.ClimateTrace.Data.Repositories.Interface;
using Microsoft.Extensions.Logging;
using NetTopologySuite.Geometries;
using Npgsql;

namespace IU.ClimateTrace.Data.Repositories
{

    public class AssetEmissionRepository : IRepository<AssetEmission>
    {
        private readonly ILogger logger;
        private readonly NpgsqlConnection connection;

        public AssetEmissionRepository(NpgsqlConnection connection,
            ILogger<AssetEmissionRepository> logger)
        {
            this.logger = logger;
            this.connection = connection;
        }

        private async Task<long> CountRecords()
        {
            try
            {
                await connection.OpenAsync();
                await using var command = new NpgsqlCommand()
                {
                    Connection = connection,
                    CommandText =
                    @"SELECT COUNT(*) FROM asset_emissions",
                };
                await command.PrepareAsync();
                var result = await command.ExecuteScalarAsync();
                if (Int64.TryParse(result?.ToString(), out long count))
                {
                    return count;
                }
                return 0;
            }
            catch (Exception ex)
            {
                logger.LogError(new EventId(), ex, "Error counting AssetEmission records");
                throw;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        public async Task<bool> Exists(AssetEmission entity)
        {
            // PKEY: iso3_country, asset_id, start_time, gas, temporal_granularity
            try
            {
                await connection.OpenAsync();
                await using var command = new NpgsqlCommand()
                {
                    Connection = connection,
                    CommandText =
                    @"SELECT EXISTS 
                        (
                            SELECT 1 FROM asset_emissions
                                WHERE iso3_country = $1
                                AND asset_id = $2
                                AND start_time = $3
                                AND gas = $4
                                AND temporal_granularity = $5
                        )",
                    Parameters =
                    {
                        new() {Value = entity.Iso3Country },
                        new() {Value = entity.Id },
                        new() {Value = entity.StartTime },
                        new() {Value = entity.Gas },
                        new() {Value = entity.TemporalGranularity },
                    }
                };
                await command.PrepareAsync();
                var result = await command.ExecuteScalarAsync();
                Boolean.TryParse(result?.ToString(), out bool didExist);
                return didExist;
            }
            catch (Exception ex)
            {
                logger.LogError(new EventId(), ex, "Error checking if AssetEmission existed");
                throw;
            }
            finally 
            {
                await connection.CloseAsync();
            }
        }

        public async Task AddAsync(AssetEmission entity)
        {
            DateTime now = DateTime.UtcNow;
            try
            {
                await connection.OpenAsync();
                await using var command = new NpgsqlCommand()
                {
                    Connection = connection,
                    CommandText = @"INSERT INTO asset_emissions (
                asset_id, 
                iso3_country, 
                original_inventory_sector, 
                start_time, 
                end_time,
                
                temporal_granularity, 
                gas, 
                emissions_quantity, 
                emissions_factor, 
                emissions_factor_units,
                
                capacity, 
                capacity_units, 
                capacity_factor, 
                activity, 
                activity_units, 
                
                source_created_date,
                source_modified_date, 
                asset_name, 
                asset_type, 
                st_astext,
                
                origin_source, 
                created_date, 
                modified_date)
                VALUES(
                    $1, $2, $3, $4, $5, $6, $7, $8, $9, $10, $11, $12, $13,
                    $14, $15, $16, $17, $18, $19, $20, $21, $22, $23
                )
                ON CONFLICT
                    ON CONSTRAINT asset_emissions_pkey
                        DO NOTHING",
                    Parameters =
                    {
                        new() {Value = entity.Id },
                        new() {Value = entity.Iso3Country },
                        new() {Value = entity.OriginalInventorySector },
                        new() {Value = entity.StartTime },
                        new() {Value = entity.EndTime},
                        new() {Value = entity.TemporalGranularity },
                        new() {Value = entity.Gas },
                        new() {Value = entity.EmissionsQuantity == null ? DBNull.Value : entity.EmissionsQuantity},
                        new() {Value = entity.EmissionsFactor == null ? DBNull.Value : entity.EmissionsFactor },
                        new() {Value = entity.EmissionsFactorUnits },
                        new() {Value = entity.Capacity == null ? DBNull.Value : entity.Capacity },
                        new() {Value = entity.CapacityUnits == null ? DBNull.Value : entity.CapacityUnits },
                        new() {Value = entity.CapacityFactor == null ? DBNull.Value : entity.CapacityFactor },
                        new() {Value = entity.Activity == null ? DBNull.Value : entity.Activity },
                        new() {Value = entity.ActivityUnits == null ? DBNull.Value : entity.ActivityUnits },
                        new() {Value = entity.SourceCreatedDate },
                        new() {Value = entity.SourceModifiedDate   == null ? DBNull.Value : entity.SourceModifiedDate },
                        new() {Value = entity.AssetName },
                        new() {Value = entity.AssetType },
                        new() {Value = entity.StAstext },
                        new() {Value = entity.origin_source },
                        new() {Value = now },
                        new() {Value = now }
                    }
                };
                await command.PrepareAsync();
                await command.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(new EventId(), ex, "Error inserting AssetEmission");
                throw;
            }
            finally { await connection.CloseAsync(); }
        }

        /// <summary>
        /// Deletes all entities from a database with a given `asset_id
        /// </summary>
        /// <param name="id">the `asset_id` of the entry in the database</param>
        public async Task DeleteAsync(int id)
        {
            try
            {
                await connection.OpenAsync();
                await using var command = new NpgsqlCommand()
                {
                    Connection = connection,
                    CommandText =
                    @"DELETE FROM asset_emissions 
                            WHERE asset_id = $1",
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
                logger.LogError(new EventId(), ex, $"Error deleting asset emission by ID {id}");
                throw;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        /// <summary>
        /// Gets a single AssetEmission by its Asset_ID
        /// 
        /// if no assetID can be found, returns null
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// an asset with the given ID
        /// 
        /// OR 
        /// 
        /// null
        /// </returns>
        public async Task<AssetEmission> GetAsync(int id)
        {
            try
            {
                await connection.OpenAsync();
                await using var command = new NpgsqlCommand()
                {
                    Connection = connection,
                    CommandText =
                    @"SELECT * FROM asset_emissions
                                WHERE asset_id = $1 LIMIT 1",
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
                        var castResult = AssetEmissionFromDataReader(reader);
                        return castResult;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                logger.LogError(new EventId(), ex, "Error getting AssetEmission");
                throw;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }




        public async Task<PagedResult<AssetEmission>> GetPagedAsync(int pageNumber = 0, int pageSize = 1000)
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
                    @"SELECT COUNT(*) FROM asset_emissions",
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
                    @"SELECT * FROM asset_emissions 
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
                List<AssetEmission> results = new();
                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        var castResult = AssetEmissionFromDataReader(reader);
                        results.Add(castResult);
                    }
                }

                return new PagedResult<AssetEmission>(results, pageNumber, pageSize, pageCount: pageCount, totalRecords: countResult);
            }
            catch (Exception ex)
            {
                logger.LogError(new EventId(), ex, "Error getting paged AssetEmission");
                throw;
            }
            finally
            {
                await connection.CloseAsync();
            }


            //try
            //{
            //    List<AssetEmission> result = new List<AssetEmission>();
            //    await connection.OpenAsync();

            //    await using var command = new NpgsqlCommand("SELECT * FROM asset_emissions LIMIT $1", connection);
            //    await using var reader = await command.ExecuteReaderAsync();
            //    while (await reader.ReadAsync())
            //    {
            //        result.Add(AssetEmissionFromDataReader(reader));
            //    }
            //    return result;
            //}
            //catch (Exception ex)
            //{
            //    throw;
            //}
            //finally
            //{
            //    await connection.CloseAsync();
            //}
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
                emissions_quantity: reader.GetDecimal(7),
                emissions_factor: reader.GetDecimal(8),
                emissions_factor_units: reader.GetString(9),
                capacity: reader.IsDBNull(10) ? 0 : reader.GetInt64(10),
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
