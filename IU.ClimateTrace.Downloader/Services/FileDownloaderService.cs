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


        /// <summary>
        /// Checks if a directory exists, and creates it if not
        /// </summary>
        /// <param name="path"></param>
        private void CreateDirectoryIfNotExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }


        /// <summary>
        /// Downloads a file from a remote server to a local destination
        /// If the destPath doesn't exist, it will be created
        /// </summary>
        /// <param name="srcUrl">the URL of the asset to be downloaded</param>
        /// <param name="destPath">the path of the file to be saved (not including the file)</param>
        /// <param name="destFileName">the filename of the file to be saved</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the srcUrl is an invalid URI
        /// </exception>
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
