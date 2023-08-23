namespace IU.ClimateTrace.Downloader.Models.Config
{
    public class ClimateTraceDownloaderSettings
    {
        public static readonly string ConfigName = "iuClimateTraceConfig";

        public required ClimateTraceDownloaderSettings_DownloadUrls DownloadUrls { get; set; } 
        public required ClimateTraceDownloaderSettings_Configurations Configurations { get; set; } 

    }

    public class ClimateTraceDownloaderSettings_DownloadUrls
    {
        public required string ForestDataUrl { get; set; }
        public required string NonForestDataUrl { get; set; }
        public required string CountryDataUrl { get; set; }
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