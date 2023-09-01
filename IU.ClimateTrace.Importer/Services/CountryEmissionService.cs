using CsvHelper;
using IU.ClimateTrace.Common.Config;
using IU.ClimateTrace.Data.Models.ClimateTraceDbModels;
using IU.ClimateTrace.Data.Repositories.Interface;
using IU.ClimateTrace.Importer.Models.ConfigModels;
using IU.ClimateTrace.Importer.Models.CsvModels;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using Newtonsoft.Json;
using System.Globalization;

namespace IU.ClimateTrace.Importer.Services
{
    /// <summary>
    ///  A service to import country emissions
    /// </summary>
    internal class CountryEmissionService : ICountryEmissionService
    {
        private readonly IRepository<CountryEmission> _countryEmissionRepository;
        private readonly IRepository<AssetEmission> _assetEmissionRepository;
        private readonly ClimateTraceDownloaderSettings _settings;
        private readonly ILogger _logger;

        public CountryEmissionService(
            IOptions<ClimateTraceDownloaderSettings> climateTraceImporterConfig,
            IRepository<CountryEmission> countryEmissionRepository,
            IRepository<AssetEmission> assetEmissionRepository,
            ILogger<CountryEmissionService> logger
            )
        {
            _settings = climateTraceImporterConfig.Value;
            _countryEmissionRepository = countryEmissionRepository;
            _assetEmissionRepository = assetEmissionRepository;
            _logger = logger;
        }


     

        public async Task UpdateCountryEmissionsFromCsvAsync()
        {
            IEnumerable<EmissionCsvEntity> records;

            var countryInventoryDataPaths = GetInventoryDataPaths();
            var availableCountries = GetAvailableCountries();
            foreach (var directoryPath in countryInventoryDataPaths.DataInventories)
            {
                foreach (var dataPath in directoryPath.Inventories)
                {
                    foreach (var country in availableCountries.CountryList)
                    {
                        string fileName = Path.Combine($"{_settings.Configurations.DownloadDataPath}",
                            "non_forest_sectors_data",
                            country.Alpha3,
                            directoryPath.Directory,
                            dataPath.FileName);

                        if (File.Exists(fileName))
                        {
                            FileInfo fi = new FileInfo(fileName);

                            using (var reader = new StreamReader(fileName))
                                if (dataPath.FileName.EndsWith("ownership.csv"))
                                {
                                    // this is an ownership record, do not import it
                                    _logger.LogWarning($"DID NOT IMPORT: '{country.Alpha3} / {directoryPath.Directory} / {dataPath.FileName}', FILE IS AN ASSET OWNERSHIP RECORD");
                                }
                                else
                                {
                                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                                    {
                                        // enable exponents, for example 7.75656e-05 to be read into decimals
                                        csv.Context.TypeConverterOptionsCache.GetOptions<decimal?>().NumberStyles = NumberStyles.Number | NumberStyles.AllowExponent;

                                        _logger.LogInformation($"Importing {country.Alpha3} / {directoryPath.Directory} / {dataPath.FileName}" +
                                            $" (size: {FileSizeFormat(fi.Length)})");

                                        while (csv.Read())
                                        {
                                            var record = csv.GetRecord<EmissionCsvEntity>();
                                            if (record == null)
                                            {
                                                _logger.LogWarning($"Null record found in: {country.Alpha3}  | {directoryPath.Directory} | {dataPath.FileName} (expected empty file)");
                                            }
                                            else
                                            {
                                                if (dataPath.FileName.StartsWith("country"))
                                                {
                                                    //var mappedRecord = MapToCountryEntityFromCsvRecord(record);
                                                    //await _countryEmissionRepository.AddAsync(mappedRecord);
                                                }
                                                else if (dataPath.FileName.StartsWith("asset"))
                                                {
                                                    // this is an asset record, upsert it
                                                    var mappedRecord = MapToAssetEntityFromCsvRecord(record);

                                                    if (! await _assetEmissionRepository.Exists(mappedRecord))
                                                    {
                                                        await _assetEmissionRepository.AddAsync(mappedRecord);
                                                    }
                                                }
                                            }
                                        }
                                        records = csv.GetRecords<EmissionCsvEntity>().ToList();
                                    }
                                }
                        }
                        else
                        {
                            _logger.LogWarning($"File not found: {country.Alpha3} / {directoryPath.Directory} / {dataPath.FileName}");
                        }
                    }
                }
            }
        }
        private static string FileSizeFormat(long fileSizeInBytes)
        {
            List<string> suffixes = new List<string> { "B", "KB", "MB", "GB", "TB", "PB" };
            int counter = 0;
            decimal tmpSize = (decimal)fileSizeInBytes;
            while (Math.Round(tmpSize / 1024) >= 1)
            {
                tmpSize = tmpSize / 1024;
                counter++;
            }
            return string.Format("{0:n1}{1}", tmpSize, suffixes[counter]);


        }

        private AssetEmission MapToAssetEntityFromCsvRecord(EmissionCsvEntity srcCsvRecord)
        {
           

            DateTime now = DateTime.UtcNow;
            var destMappedObj = new AssetEmission(
                asset_id: srcCsvRecord.AssetId,
               iso3_country: srcCsvRecord.Iso3Country,
               original_inventory_sector: srcCsvRecord.OriginalInventorySector,
               start_time: srcCsvRecord.StartTime,
               end_time: srcCsvRecord.EndTime,
               temporal_granularity: srcCsvRecord.TemporalGranularity,
               gas: srcCsvRecord.Gas,
               emissions_quantity: srcCsvRecord.EmissionsQuantity,
               emissions_factor: srcCsvRecord.EmissionsFactor,
               emissions_factor_units: srcCsvRecord.EmissionsFactorUnits,
               capacity: srcCsvRecord.Capacity,
               capacity_units: srcCsvRecord.CapacityUnits,
               capacity_factor: srcCsvRecord.CapacityFactor,
               activity: srcCsvRecord.Activity,
               activity_units: srcCsvRecord.ActivityUnits,
               origin_source: "climate_trace",
               source_created_date: srcCsvRecord.CreatedDate,
               source_modified_date: srcCsvRecord.ModifiedDate,
               created_date: now,
               modified_date: null,
               asset_name: srcCsvRecord.AssetName,
               asset_type: srcCsvRecord.AssetType,
               st_astext: null
           );
            try
            {
                // read the WellKnownText Geometry from the csv into a Geometry obj
                WKTReader wKTReader = new WKTReader();

                Geometry geom = wKTReader.Read(srcCsvRecord.StAstex);
                destMappedObj.StAstext = geom;
            }
            catch (Exception ex)
            {
                throw;
            }

            return destMappedObj;
        }

        private CountryEmission MapToCountryEntityFromCsvRecord(EmissionCsvEntity srcCsvRecord)
        {

            var destMappedObj = new CountryEmission(
                iso3_country: srcCsvRecord.Iso3Country,
                start_time: srcCsvRecord.StartTime,
                end_time: srcCsvRecord.EndTime,
                original_inventory_sector: srcCsvRecord.OriginalInventorySector,
                gas: srcCsvRecord.Gas,
                emissions_quantity: (decimal)(srcCsvRecord.EmissionsQuantity == null ? 0 : srcCsvRecord.EmissionsQuantity),
                emissions_quantity_units: srcCsvRecord.EmissionsQuantityUnits,
                temporal_granularity: srcCsvRecord.TemporalGranularity,
                origin_source: "climate_trace",
                source_created_date: srcCsvRecord.CreatedDate,
                source_modified_date: srcCsvRecord.ModifiedDate
            );
            return destMappedObj;

        }

        private CountryInventoryJsonObj GetAvailableCountries()
        {
            var dataInventoriesJsonPath = Path.Combine(Directory.GetCurrentDirectory(),
                "InventoryFiles\\InventoryLists\\country-list.json");

            CountryInventoryJsonObj? dataInvObject;
            using (StreamReader sr = File.OpenText(dataInventoriesJsonPath))
            {
                dataInvObject = JsonConvert.DeserializeObject<CountryInventoryJsonObj>(sr.ReadToEnd());
            }
            return dataInvObject;
        }
       

        private DataInventoryJsonObj GetInventoryDataPaths()
        {
            var dataInventoriesJsonPath = Path.Combine(Directory.GetCurrentDirectory(),
                "InventoryFiles\\InventoryLists\\data-inventories.dev.json");

            DataInventoryJsonObj? dataInvObject;
            using (StreamReader sr = File.OpenText(dataInventoriesJsonPath))
            {
                dataInvObject = JsonConvert.DeserializeObject<DataInventoryJsonObj>(sr.ReadToEnd());
            }
            return dataInvObject;
        }
    }

    


    


    
}
