using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IU.ClimateTrace.Downloader.Services
{
    public class FileUnzipperService : IFileUnzipperService
    {
        public void UnzipFile(string srcPath, string destPath)
        {
            System.IO.Compression.ZipFile.ExtractToDirectory(srcPath, destPath);
        }
    }
}
