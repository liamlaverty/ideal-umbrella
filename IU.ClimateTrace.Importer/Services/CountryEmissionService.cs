using CsvHelper;
using CsvHelper.Configuration.Attributes;
using IU.ClimateTrace.Common.Config;
using IU.ClimateTrace.Data.Models.ClimateTraceDbModels;
using IU.ClimateTrace.Data.Repositories.Interface;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace IU.ClimateTrace.Importer.Services
{
    /// <summary>
    ///  A service to import country emissions
    /// </summary>
    internal class CountryEmissionService : ICountryEmissionService
    {
        private readonly IRepository<CountryEmission> _countryEmissionRepository;
        private readonly ClimateTraceDownloaderSettings _settings;

        public CountryEmissionService(
            IOptions<ClimateTraceDownloaderSettings> climateTraceImporterConfig,
            IRepository<CountryEmission> countryEmissionRepository)
        {
            _settings = climateTraceImporterConfig.Value;
            _countryEmissionRepository = countryEmissionRepository;
        }


     

        public async Task UpdateCountryEmissionsFromCsvAsync()
        {
            IEnumerable<EmissionCsvEntity> records;

            var countryInventoryDataPaths = GetInventoryDataPaths();
            var availableCountries = GetAvailableCountries();
            foreach (var directoryPath in countryInventoryDataPaths.dataInventories)
            {
                foreach (var dataPath in directoryPath.inventories)
                {
                    foreach (var country in availableCountries.countryList)
                    {
                        if (File.Exists(
                            Path.Combine($"{_settings.Configurations.DownloadDataPath}",
                            "non_forest_sectors_data",
                            country.alpha3,
                            directoryPath.directory,
                            dataPath.fileName)))
                        {
                            using (var reader = new StreamReader(
                                Path.Combine($"{_settings.Configurations.DownloadDataPath}",
                                "non_forest_sectors_data",
                                country.alpha3,
                                directoryPath.directory,
                                dataPath.fileName)))

                            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                            {
                                records = csv.GetRecords<EmissionCsvEntity>().ToList();
                            }
                            Console.WriteLine($"Read {country.alpha3} / {directoryPath.directory} / {dataPath.fileName}");
                        }
                        else
                        {
                            Console.WriteLine($"File not found: {country.alpha3} / {directoryPath.directory} / {dataPath.fileName}");

                        }
                    }
                }
            }
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

    public class CountryInventoryJsonObj
    {
        public Countrylist[] countryList { get; set; }
    }
    public class Countrylist
    {
        public string name { get; set; }
        public string alpha3 { get; set; }
        public string countryCode { get; set; }
    }


    public class DataInventoryJsonObj
    {
        public Datainventory[] dataInventories { get; set; }
    }

    public class Datainventory
    {
        public Inventory[] inventories { get; set; }
        public string directory { get; set; }
    }

    public class Inventory
    {
        public string fileName { get; set; }
        public string[] csvColumns { get; set; }
    }

    public class CountryInventoryEntity
    {
        public required string Name { get; set; }
        public required string Alpha3 { get; set; }
        public required string CountryCode { get; set; }
    }


    [Delimiter(",")]
    public class EmissionCsvEntity
    {

        [Optional]
        [Name("asset_id")]
        public string AssetId { get; set; }

        [Optional]
        [Name("iso3_country")]
        public string Iso3Country { get; set; }

        [Optional]
        [Name("original_inventory_sector")]
        public string OriginalInventorySector { get; set; }

        [Optional]
        [Name("start_time")]
        public string StartTime { get; set; }

        [Optional]
        [Name("end_time")]
        public string EndTime { get; set; }

        [Optional]
        [Name("temporal_granularity")]
        public string TemporalGranularity { get; set; }

        [Optional]
        [Name("gas")]
        public string Gas { get; set; }

        [Optional]
        [Name("emissions_quantity")]
        public string EmissionsQuantity { get; set; }

        [Optional]
        [Name("emissions_factor")]
        public string EmissionsFactor { get; set; }

        [Optional]
        [Name("emissions_factor_units")]
        public string EmissionsFactorUnits { get; set; }

        [Optional]
        [Name("capacity")]
        public string Capacity { get; set; }

        [Optional]
        [Name("capacity_units")]
        public string CapacityUnits { get; set; }

        [Optional]
        [Name("capacity_factor")]
        public string CapacityFactor { get; set; }

        [Optional]
        [Name("activity")]
        public string Activity { get; set; }

        [Optional]
        [Name("activity_units")]
        public string ActivityUnits { get; set; }

        [Optional]
        [Name("created_date")]
        public string CreatedDate { get; set; }

        [Optional]
        [Name("modified_date")]
        public string ModifiedDate { get; set; }

        [Optional]
        [Name("asset_name")]
        public string AssetName { get; set; }

        [Optional]
        [Name("asset_type")]
        public string AssetType { get; set; }

        [Optional]
        [Name("st_astex")]
        public string StAstex { get; set; }


    }
}
