using IU.ClimateTrace.Common.Config;
using IU.ClimateTrace.Data.Models.ClimateTraceDbModels;
using IU.ClimateTrace.Data.Repositories.Interface;
using IU.ClimateTrace.Importer.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace IU.ClimateTrace.Importer
{
    public interface IClimateTraceImporter
    {
        Task ImportData();
        Task PrintSampleData();
    }
    public class ClimateTraceImporter : IClimateTraceImporter
    {
        private readonly ClimateTraceDownloaderSettings _settings;
        private readonly IRepository<AssetEmission> _assetEmissionRepository;
        private readonly IRepository<CountryEmission> _countryEmissionRepository;
        private readonly ICountryEmissionService _countryEmissionService;
        private readonly ILogger<ClimateTraceImporter> _logger;

        public ClimateTraceImporter(IOptions<ClimateTraceDownloaderSettings> climateTraceImporterConfig,
            IRepository<AssetEmission> assetEmissionRepository,
            IRepository<CountryEmission> countryEmissionRepository,
            ICountryEmissionService countryEmissionService, 
            ILogger<ClimateTraceImporter> logger) 
        {
            _logger = logger;
            _logger.LogInformation($"starting {nameof(ClimateTraceImporter)}");

            _settings = climateTraceImporterConfig.Value;
            _assetEmissionRepository = assetEmissionRepository;
            _countryEmissionRepository = countryEmissionRepository;
            _countryEmissionService = countryEmissionService;
        }

        public async Task ImportData()
        {
            await _countryEmissionService.UpdateCountryEmissionsFromCsvAsync();
        }

        public async Task PrintSampleData()
        {
            _logger.LogInformation("Printing sample data");


            // Check GetAsync runs
            var assetEntity = await _assetEmissionRepository.GetAsync(1835962831);
            _logger.LogInformation($"Asset emission Get Sample: {assetEntity.Id}");


            // Check GetPaged runs
            int pageNumber = 1;
            int pageSize = 5;
            var assetEntityPagedResult = await _assetEmissionRepository.GetPagedAsync(pageNumber: pageNumber, pageSize: pageSize);
            _logger.LogInformation($"Asset emission GetPaged Sample (total records): {assetEntityPagedResult.TotalRecords}");

            // check Exists runs
            var sampleEntityDoesExist = await _assetEmissionRepository.Exists(assetEntity);
            _logger.LogInformation($"Sample Asset entity exists: {sampleEntityDoesExist}");



            // Check GetAsync runs
            var countryEntity = await _countryEmissionRepository.GetAsync(1);
            _logger.LogInformation($"Country emission Get Sample: {countryEntity.Id}");


            // Check GetPaged runs
            var countryEntityPagedResult = await _countryEmissionRepository.GetPagedAsync(pageNumber: pageNumber, pageSize: pageSize);
            _logger.LogInformation($"Country emission GetPaged Sample (total records): {countryEntityPagedResult.TotalRecords}");

            // check Exists runs
            var samplecountryEntityDoesExist = await _countryEmissionRepository.Exists(countryEntity);
            _logger.LogInformation($"Sample Country entity exists: {samplecountryEntityDoesExist}");



            _logger.LogInformation($"Checking GetPaged CountryEmission multiple times:");

            for (int i = 0; i < 2; i++)
            {
                var countryResult = await _countryEmissionRepository.GetPagedAsync(pageSize: 5);
                foreach (var item in countryResult.Results)
                {
                    Console.WriteLine($"{item.Iso3Country}");
                }
            }

            _logger.LogInformation($"Checking GetPaged AssetEmission multiple times:");
            for (int i = 0; i < 2; i++)
            {
                var assetResult = await _assetEmissionRepository.GetPagedAsync(pageSize: 5);

                foreach (var item in assetResult.Results)
                {
                    _logger.LogInformation($"{item.AssetName} - StAstext is null? {(item.StAstext == null ? "true" : "false")}");
                }
            }
        }
    }
}