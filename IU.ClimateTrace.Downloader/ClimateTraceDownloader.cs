using IU.ClimateTrace.Downloader.Models.Config;
using Microsoft.Extensions.Options;

namespace IU.ClimateTrace.Downloader
{

    public interface IClimateTraceDownloader
    {
        string DownloadData();
    }


    public class ClimateTraceDownloader : IClimateTraceDownloader
    {
        private ClimateTraceDownloaderSettings _settings;

        public ClimateTraceDownloader(IOptions<ClimateTraceDownloaderSettings> climateTraceDownloaderConfig)
        {
            _settings = climateTraceDownloaderConfig.Value;

            Console.WriteLine("Configuring download paths");

            var nonForestDataPath = Path.Combine(_settings.Configurations.DownloadDataPath, "data_packages", "climate_trace", "sector_packages", "non_forest_sectors_data");
            Console.WriteLine($"nonForestDataPath: {nonForestDataPath}");

            var forestDataPath = Path.Combine(_settings.Configurations.DownloadDataPath, "data_packages", "climate_trace", "sector_packages", "forest_sectors_data");
            Console.WriteLine($"forestDataPath: {forestDataPath}");

            var countriesDataPath = Path.Combine(_settings.Configurations.DownloadDataPath, "data_packages", "climate_trace", "country_packages", "country_sector_data");
            Console.WriteLine($"countriesDataPath: {countriesDataPath}");
        }

        List<string> CountryThreeChar = new List<string> { "GBR", "NOR" };

        public string DownloadData()
        {
            if (_settings.Configurations.EnableDownloadCountryData)
            {
                foreach (var country in CountryThreeChar)
                {
                    DownloadSingleCountryData(country);
                }
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
            }
            else
            {
                Console.WriteLine($"EnableDownloadNonForestryData was set to {_settings.Configurations.EnableDownloadNonForestryData}, skipping download");
            }

            if (_settings.Configurations.EnableUnzipAfterDownload)
            {
                UnzipFiles();
            }
            else
            {
                Console.WriteLine($"EnableUnzipAfterDownload was set to {_settings.Configurations.EnableUnzipAfterDownload}, skipping unzip");
            }

            return "complete";
        }

        private void DownloadSingleCountryNonForestSectorData(string countryThreeCharName)
        {
            Console.WriteLine($"Downloading non-forest data for country {countryThreeCharName}");
        }

        private void DownloadSingleCountryForestSectorData(string countryThreeCharName)
        {
            Console.WriteLine($"Downloading forest data for country {countryThreeCharName}");
        }

        private void DownloadSingleCountryData(string countryThreeCharName)
        {
            Console.WriteLine($"Downloading data for country {countryThreeCharName}");
        }

        private void UnzipFiles()
        {
            Console.WriteLine($"Unzipping files");
        }

       

    }
}