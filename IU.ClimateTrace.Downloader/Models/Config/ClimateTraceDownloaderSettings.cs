namespace IU.ClimateTrace.Downloader.Models.Config
{
    public class ClimateTraceDownloaderSettings
    {
        public static readonly string ConfigName = "iuClimateTraceConfig";

        public ClimateTraceDownloaderSettings_DownloadUrls DownloadUrls { get; set; }
        public ClimateTraceDownloaderSettings_Configurations Configurations { get; set; }

    }

    public class ClimateTraceDownloaderSettings_DownloadUrls
    {
        public string ForestDataUrl { get; set; }
        public string NonForestDataUrl { get; set; }
        public string CountryDataUrl { get; set; }
    }
    public class ClimateTraceDownloaderSettings_Configurations
    {
        public bool EnableDownloadCountryData { get; set; }
        public bool EnableDownloadForestryData { get; set; }
        public bool EnableDownloadNonForestryData { get; set; }
        public bool EnableUnzipAfterDownload { get; set; }
        public string DownloadDataPath { get; set; }
    }
}