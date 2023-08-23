using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IU.ClimateTrace.Downloader.Services
{
    public interface IFileDownloaderService
    {
        /// <summary>
        /// Downloads a single file
        /// </summary>
        /// <param name="srcUrl">The url of the path</param>
        /// <param name="destPath">The filesystem path to save to</param>
        void DownloadFile(string srcUrl, string destPath);
    }
}
