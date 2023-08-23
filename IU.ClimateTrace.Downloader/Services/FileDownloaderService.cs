using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IU.ClimateTrace.Downloader.Services
{
    public class FileDownloaderService : IFileDownloaderService
    {
        private readonly HttpClient _httpClient;

        public FileDownloaderService()
        { 
            // TODO: replace with httpclientfactory
            _httpClient = new HttpClient();
            _httpClient.Timeout = TimeSpan.FromMinutes(10);
        }

        private void CreateDirectoryIfNotExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public async Task DownloadFileAsync(string srcUrl, string destPath, string destFileName)
        {
            if (!Uri.TryCreate(srcUrl, UriKind.Absolute, out var uri))
            {
                throw new InvalidOperationException($"'{srcUrl}' is an invalid Uri");
            }
            
            string destFile = Path.Combine(destPath, destFileName);
            CreateDirectoryIfNotExists(destPath);

            Console.WriteLine($"Downloading '{uri}' to '{destFile}'");
            byte[] fileBytes = await _httpClient.GetByteArrayAsync(uri);
            await File.WriteAllBytesAsync(destFile, fileBytes);
        }
    }
}
