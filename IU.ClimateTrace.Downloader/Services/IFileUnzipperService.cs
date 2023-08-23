namespace IU.ClimateTrace.Downloader.Services
{
    public interface IFileUnzipperService
    {
        /// <summary>
        /// Unzips a file into a directory
        /// </summary>
        /// <param name="srcPath">the path of the zip file</param>
        /// <param name="destPath">the path to unzip to</param>
        void UnzipFile(string srcPath, string destPath);

    }
}
