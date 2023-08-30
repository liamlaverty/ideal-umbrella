using IU.ClimateTrace.Common.Config;
using IU.ClimateTrace.Data.Models.ClimateTraceDbModels;
using IU.ClimateTrace.Data.Repositories.Interface;
using Microsoft.Extensions.Options;

namespace IU.ClimateTrace.Importer
{
    public interface IClimateTraceImporter
    {
        Task ImportData();
    }
    public class ClimateTraceImporter : IClimateTraceImporter
    {
        private readonly ClimateTraceDownloaderSettings _settings;
        private readonly IRepository<AssetEmission> _assetEmissionRepository;
        private readonly IRepository<CountryEmission> _countryEmissionRepository;

        public ClimateTraceImporter(IOptions<ClimateTraceDownloaderSettings> climateTraceImporterConfig,
            IRepository<AssetEmission> assetEmissionRepository,
            IRepository<CountryEmission> countryEmissionRepository) 
        {
            Console.WriteLine($"starting {nameof(ClimateTraceImporter)}");

            _settings = climateTraceImporterConfig.Value;
            _assetEmissionRepository = assetEmissionRepository;
            _countryEmissionRepository = countryEmissionRepository;
        }

        public async Task ImportData()
        {
            for (int i = 0; i < 200; i++)
            {
                var countryResult = await _countryEmissionRepository.GetAllAsync();
                foreach (var item in countryResult)
                {
                    Console.WriteLine($"{item.Iso3Country}");
                }
            }
            for (int i = 0; i < 200; i++)
            {
                var assetResult = await _assetEmissionRepository.GetAllAsync();


                // var assetResult = await GetAssetEmissions();
                foreach (var item in assetResult)
                {
                    Console.WriteLine($"{item.AssetName} - StAstext is null? {(item.StAstext == null ? "true" : "false")}");
                }
            }
        }
    }
}