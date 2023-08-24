using IU.ClimateTrace.Downloader.Models.Config;
using IU.ClimateTrace.Downloader.Services;
using Microsoft.Extensions.Options;

namespace IU.ClimateTrace.Downloader
{

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

            nonForestDataPath = Path.Combine(_settings.Configurations.DownloadDataPath, "non_forest_sectors_data");
            forestDataPath = Path.Combine(_settings.Configurations.DownloadDataPath, "forest_sectors_data");
            countriesDataPath = Path.Combine(_settings.Configurations.DownloadDataPath, "country_sector_data");
        }


        public async Task<DownloaderResult> DownloadData()
        {
            if (_settings.Configurations.EnableDownloadCountryData)
            {
                await DownloadCountryData();
                downloaderResult.DownloadedCountryData = true;
            }

            if (_settings.Configurations.EnableDownloadForestryData)
            {
                foreach (var country in _settings.DownloadConfiguration.SpecifyCountries)
                {
                    await DownloadSingleCountryForestSectorData(country);
                }
                downloaderResult.DownloadedForestData = true;
            }

            if (_settings.Configurations.EnableDownloadNonForestryData)
            {
                foreach (var country in _settings.DownloadConfiguration.SpecifyCountries)
                {
                    await DownloadSingleCountryNonForestSectorData(country);
                }
                downloaderResult.DownloadedNonForestData = true;
            }

            return downloaderResult;
        }

        

        private async Task DownloadSingleCountryForestSectorData(string countryThreeCharName)
        {
            // downloads a file from https://downloads.climatetrace.org/country_packages/forest_sectors/AFG.zip
            // where AFG.zip is {countryThreeCharName}.zip
            Console.WriteLine($"Downloading forest data for country {countryThreeCharName}");
            string remoteUrl = $"{_settings.DownloadConfiguration.NonForestDataUrl}{countryThreeCharName}.zip";
            await _fileDownloader.DownloadFileAsync(
                remoteUrl,
                Path.Combine(forestDataPath),
                $"{countryThreeCharName}.zip"
                );
            if (_settings.Configurations.EnableUnzipAfterDownload)
            {
                _fileUnzipper.UnzipFile(
                    Path.Combine(
                        forestDataPath,
                        $"{countryThreeCharName}.zip"),
                    Path.Combine(
                        forestDataPath,
                        $"{countryThreeCharName}")
                    );
            }
        }

        private async Task DownloadSingleCountryNonForestSectorData(string countryThreeCharName)
        {
            // downloads a file from https://downloads.climatetrace.org/country_packages/non_forest_sectors/AFG.zip
            // where AFG.zip is {countryThreeCharName}.zip
            Console.WriteLine($"Downloading non-forest data for country {countryThreeCharName}");
            string remoteUrl = $"{_settings.DownloadConfiguration.NonForestDataUrl}{countryThreeCharName}.zip";
            await _fileDownloader.DownloadFileAsync(
                remoteUrl,
                Path.Combine(nonForestDataPath),
                $"{countryThreeCharName}.zip"
                );
            if (_settings.Configurations.EnableUnzipAfterDownload)
            {
                _fileUnzipper.UnzipFile(
                    Path.Combine(
                        nonForestDataPath,
                        $"{countryThreeCharName}.zip"),
                    Path.Combine(
                        nonForestDataPath,
                        $"{countryThreeCharName}")
                    );
            }
        }

        private async Task DownloadCountryData()
        {
            foreach (var fileName in _settings.DownloadConfiguration.CountryDataDownloadFileSets)
            {
                await _fileDownloader.DownloadFileAsync(
                    $"{_settings.DownloadConfiguration.ClimateTraceBaseUrl}sector_packages/{fileName}.zip",
                    Path.Combine(
                        countriesDataPath, "sector_packages"),
                        $"{fileName}.zip"
                    );
                if (_settings.Configurations.EnableUnzipAfterDownload)
                {
                    _fileUnzipper.UnzipFile(
                        Path.Combine(
                            countriesDataPath, "sector_packages",
                            $"{fileName}.zip"),
                        Path.Combine(
                            countriesDataPath, "sector_packages",
                            $"{fileName}")
                        );
                }
            }

            Console.WriteLine($"Downloading data for country");
        }
    }
}