namespace IU.ClimateTrace.Downloader
{

    public interface IClimateTraceDownloader
    {
        string DownloadData();
    }


    public class ClimateTraceDownloader : IClimateTraceDownloader
    {
        public string DownloadData()
        {
            return "complete";
        }
    }
}