using IU.ClimateTrace.Common.Config;
using IU.ClimateTrace.Data.Models.ClimateTraceDbModels;
using IU.ClimateTrace.Data.Repositories.Interface;
using IU.ClimateTrace.Importer.Services;
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

        public ClimateTraceImporter(IOptions<ClimateTraceDownloaderSettings> climateTraceImporterConfig,
            IRepository<AssetEmission> assetEmissionRepository,
            IRepository<CountryEmission> countryEmissionRepository,
            ICountryEmissionService countryEmissionService) 
        {
            Console.WriteLine($"starting {nameof(ClimateTraceImporter)}");

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
            //for (int i = 0; i < 2; i++)
            //{
            //    var countryResult = await _countryEmissionRepository.GetPagedAsync();
            //    foreach (var item in countryResult.Results)
            //    {
            //        Console.WriteLine($"{item.Iso3Country}");
            //    }
            //}
            for (int i = 0; i < 2; i++)
            {
                var assetResult = await _assetEmissionRepository.GetPagedAsync();

                foreach (var item in assetResult.Results)
                {
                    Console.WriteLine($"{item.AssetName} - StAstext is null? {(item.StAstext == null ? "true" : "false")}");
                }
            }
        }
    }
}