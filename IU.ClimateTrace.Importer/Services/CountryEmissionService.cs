using CsvHelper;
using CsvHelper.Configuration.Attributes;
using IU.ClimateTrace.Common.Config;
using IU.ClimateTrace.Data.Models.ClimateTraceDbModels;
using IU.ClimateTrace.Data.Repositories.Interface;
using IU.ClimateTrace.Importer.Models.ConfigModels;
using IU.ClimateTrace.Importer.Models.CsvModels;
using Microsoft.Extensions.Options;
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
            foreach (var directoryPath in countryInventoryDataPaths.DataInventories)
            {
                foreach (var dataPath in directoryPath.Inventories)
                {
                    foreach (var country in availableCountries.CountryList)
                    {
                        if (File.Exists(
                            Path.Combine($"{_settings.Configurations.DownloadDataPath}",
                            "non_forest_sectors_data",
                            country.Alpha3,
                            directoryPath.Directory,
                            dataPath.FileName)))
                        {
                            using (var reader = new StreamReader(
                                Path.Combine($"{_settings.Configurations.DownloadDataPath}",
                                "non_forest_sectors_data",
                                country.Alpha3,
                                directoryPath.Directory,
                                dataPath.FileName)))

                            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                            {
                                records = csv.GetRecords<EmissionCsvEntity>().ToList();
                            }
                            Console.WriteLine($"Read {country.Alpha3} / {directoryPath.Directory} / {dataPath.FileName}");
                        }
                        else
                        {
                            Console.WriteLine($"File not found: {country.Alpha3} / {directoryPath.Directory} / {dataPath.FileName}");

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

    


    


    
}
