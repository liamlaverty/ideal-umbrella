using IU.ClimateTrace.Downloader.Models.Config;
using IU.ClimateTrace.Downloader.Services;
using Microsoft.Extensions.Options;

namespace IU.ClimateTrace.Downloader
{

    public interface IClimateTraceDownloader
    {
        Task<DownloaderResult> DownloadData();
    }

    public class DownloaderResult
    {
        public bool DownloadedCountryData { get; set; } = false;
        public bool DownloadedForestData { get; set; } = false;
        public bool DownloadedNonForestData { get; set; } = false;
        public bool UnzippedData { get; set; } = false;
    }


    public class ClimateTraceDownloader : IClimateTraceDownloader
    {
        private ClimateTraceDownloaderSettings _settings;
        private DownloaderResult downloaderResult;
        List<string> CountryThreeChar = new List<string> { "GBR", "NOR" };
        private string nonForestDataPath;
        private string forestDataPath;
        private string countriesDataPath;

        private readonly IFileDownloaderService _fileDownloader;
        private readonly IFileUnzipperService _fileUnzipper;

        public ClimateTraceDownloader(
            IOptions<ClimateTraceDownloaderSettings> climateTraceDownloaderConfig, 
            IFileUnzipperService fileUnzipper, 
            IFileDownloaderService fileDownloader)
        {
            _fileDownloader = fileDownloader;
            _fileUnzipper = fileUnzipper;

            _settings = climateTraceDownloaderConfig.Value;

            downloaderResult = new DownloaderResult();

            nonForestDataPath = Path.Combine(_settings.Configurations.DownloadDataPath, "data_packages", "climate_trace", "sector_packages", "non_forest_sectors_data");
            Console.WriteLine($"nonForestDataPath: {nonForestDataPath}");

            forestDataPath = Path.Combine(_settings.Configurations.DownloadDataPath, "data_packages", "climate_trace", "sector_packages", "forest_sectors_data");
            Console.WriteLine($"forestDataPath: {forestDataPath}");

            countriesDataPath = Path.Combine(_settings.Configurations.DownloadDataPath, "data_packages", "climate_trace", "country_packages", "country_sector_data");
            Console.WriteLine($"countriesDataPath: {countriesDataPath}");
        }


        public async Task<DownloaderResult> DownloadData()
        {
            if (_settings.Configurations.EnableDownloadCountryData)
            {
                await DownloadCountryData();
                downloaderResult.DownloadedCountryData = true;
            }
            else
            {
                Console.WriteLine($"EnableDownloadCountryData was set to {_settings.Configurations.EnableDownloadCountryData}, skipping download");
            }

            if (_settings.Configurations.EnableDownloadForestryData)
            {
                foreach (var country in CountryThreeChar)
                {
                    DownloadSingleCountryForestSectorData(country);
                }
                downloaderResult.DownloadedForestData = true;

            }
            else
            {
                Console.WriteLine($"EnableDownloadForestryData was set to {_settings.Configurations.EnableDownloadForestryData}, skipping download");
            }


            if (_settings.Configurations.EnableDownloadNonForestryData)
            {
                foreach (var country in CountryThreeChar)
                {
                    DownloadSingleCountryNonForestSectorData(country);
                }
                downloaderResult.DownloadedNonForestData = true;

            }
            else
            {
                Console.WriteLine($"EnableDownloadNonForestryData was set to {_settings.Configurations.EnableDownloadNonForestryData}, skipping download");
            }

            if (_settings.Configurations.EnableUnzipAfterDownload)
            {
                UnzipFiles();
                downloaderResult.UnzippedData = true;
            }
            else
            {
                Console.WriteLine($"EnableUnzipAfterDownload was set to {_settings.Configurations.EnableUnzipAfterDownload}, skipping unzip");
            }

            return downloaderResult;
        }

        private void DownloadSingleCountryNonForestSectorData(string countryThreeCharName)
        {
            Console.WriteLine($"Downloading non-forest data for country {countryThreeCharName}");
        }

        private void DownloadSingleCountryForestSectorData(string countryThreeCharName)
        {
            Console.WriteLine($"Downloading forest data for country {countryThreeCharName}");
        }

        private async Task DownloadCountryData()
        {
            List<string> remoteFileNames =
                new List<string>()
                {
                    "buildings.zip",
                    "fossil_fuel_operations.zip",
                    "manufacturing.zip",
                    "power.zip",
                    "waste.zip",
                    "agriculture.zip",
                    "transportation.zip",
                    "forestry_and_land_use.zip",
                };

            foreach (var fileName in remoteFileNames)
            {
                await _fileDownloader.DownloadFileAsync(
                    $"{_settings.DownloadUrls.CountryDataUrl}sector_packages/{fileName}",
                    Path.Combine(countriesDataPath, "sector_packages"),
                    fileName
                    );
            }

            Console.WriteLine($"Downloading data for country");
        }

        private void UnzipFiles()
        {
            Console.WriteLine($"Unzipping files");
        }

       

    }
}