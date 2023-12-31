﻿namespace IU.ClimateTrace.Downloader.Services
{
    public interface IFileDownloaderService
    {
        /// <summary>
        /// Downloads a single file
        /// </summary>
        /// <param name="srcUrl">The url of the path</param>
        /// <param name="destPath">The filesystem path to save to</param>
        Task DownloadFileAsync(string srcUrl, string destPath, string destFileName);
    }
}
