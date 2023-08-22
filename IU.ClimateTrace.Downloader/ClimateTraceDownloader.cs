namespace IU.ClimateTrace.Downloader
{




    public interface IClimateTraceDownloader
    {
        string DownloadData();
    }


    public class ClimateTraceDownloader : IClimateTraceDownloader
    {

        private string CTDownloadUrl_ForestData = "https://downloads.climatetrace.org/country_packages/forest/";
        private string CTDownloadUrl_NonForestData = "https://downloads.climatetrace.org/country_packages/non_forest_sectors/";
        private string CTDownloadUrl_CountryData = "https://downloads.climatetrace.org/";
        
        
        private string DestinationBasePath = "C:\\Users\\liaml\\Documents\\GitHub\\CTDataDownloads";

        private bool DownloadCountryData= true;
        private bool DownloadForestSectorData = true;
        private bool DownloadNonForestSectorData = true;
        private bool UnzipFilesAfterDownload = true;

        private string nonForestDataPath;
        private string forestDataPath;
        private string countriesDataPath;

        public ClimateTraceDownloader()
        {
            Console.WriteLine("Configuring download paths");

            var nonForestDataPath = Path.Combine(DestinationBasePath, "data_packages", "climate_trace", "sector_packages", "non_forest_sectors_data");
            Console.WriteLine($"nonForestDataPath: {nonForestDataPath}");

            var forestDataPath = Path.Combine(DestinationBasePath, "data_packages", "climate_trace", "sector_packages", "forest_sectors_data");
            Console.WriteLine($"forestDataPath: {forestDataPath}");

            var countriesDataPath = Path.Combine(DestinationBasePath, "data_packages", "climate_trace", "country_packages", "country_sector_data");
            Console.WriteLine($"countriesDataPath: {countriesDataPath}");
        }

        List<string> CountryThreeChar = new List<string> { "GBR", "NOR" };

        public string DownloadData()
        {
            if (DownloadCountryData)
            {
                foreach (var country in CountryThreeChar)
                {
                    DownloadSingleCountryData(country);
                }
            }

            if (DownloadForestSectorData)
            {
                foreach (var country in CountryThreeChar)
                {
                    DownloadSingleCountryForestSectorData(country);
                }
            }

            if (DownloadNonForestSectorData)
            {
                foreach (var country in CountryThreeChar)
                {
                    DownloadSingleCountryNonForestSectorData(country);
                }
            }

            if (UnzipFilesAfterDownload)
            {
                UnzipFiles();
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