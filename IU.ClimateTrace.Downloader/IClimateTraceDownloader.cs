namespace IU.ClimateTrace.Downloader
{
    public interface IClimateTraceDownloader
    {
        Task<DownloaderResult> DownloadData();
    }
}