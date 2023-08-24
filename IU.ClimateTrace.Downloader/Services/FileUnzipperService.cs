namespace IU.ClimateTrace.Downloader.Services
{
    internal class FileUnzipperService : IFileUnzipperService
    {
        public void UnzipFile(string srcPath, string destPath)
        {
            if (Directory.Exists(destPath))
            {
                Directory.Delete(destPath, recursive: true);
            }
            Console.WriteLine($"unzipping '{srcPath}' to '{destPath}'");
            System.IO.Compression.ZipFile.ExtractToDirectory(srcPath, destPath);
        }
    }
}
