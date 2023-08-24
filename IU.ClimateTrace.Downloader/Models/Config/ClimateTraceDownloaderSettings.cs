namespace IU.ClimateTrace.Downloader.Models.Config
{
    public class ClimateTraceDownloaderSettings
    {
        public static readonly string ConfigName = "iuClimateTraceConfig";

        public required ClimateTraceDownloaderSettings_DownloadConfiguration DownloadConfiguration { get; set; } 
        public required ClimateTraceDownloaderSettings_Configurations Configurations { get; set; } 

    }

    public class ClimateTraceDownloaderSettings_DownloadConfiguration
    {
        public required string ForestDataUrl { get; set; }
        public required string NonForestDataUrl { get; set; }
        public required string ClimateTraceBaseUrl { get; set; }
        public required IEnumerable<string> CountryDataDownloadFileSets { get; set; }
        public required IEnumerable<string> SpecifyCountries { get; set; }
    }
    public class ClimateTraceDownloaderSettings_Configurations
    {
        public bool EnableDownloadCountryData { get; set; }
        public bool EnableDownloadForestryData { get; set; }
        public bool EnableDownloadNonForestryData { get; set; }
        public bool EnableUnzipAfterDownload { get; set; }
        public required string DownloadDataPath { get; set; }
    }
}