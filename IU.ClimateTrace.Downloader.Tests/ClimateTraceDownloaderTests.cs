using IU.ClimateTrace.Downloader.Models.Config;
using IU.ClimateTrace.Downloader.Services;
using Microsoft.Extensions.Options;
using Moq;

namespace IU.ClimateTrace.Downloader.Tests
{
    [TestClass]
    public class ClimateTraceDownloaderTests
    {

        private ClimateTraceDownloader CreateClimateTraceDownloader()
        {
            Mock<IFileDownloaderService> mockFileDownloader = new Mock<IFileDownloaderService>(); 
            Mock<IFileUnzipperService> mockFileUnzipper = new Mock<IFileUnzipperService>(); 

            IOptions<ClimateTraceDownloaderSettings> sampleOptions =
                Options.Create(new ClimateTraceDownloaderSettings
                {
                    Configurations = new()
                    {
                        EnableUnzipAfterDownload = true,
                        DownloadDataPath = "",
                        EnableDownloadCountryData = true,
                        EnableDownloadForestryData = true,
                        EnableDownloadNonForestryData = true,
                    },
                    DownloadUrls = new()
                    {
                        CountryDataUrl = "",
                        ForestDataUrl = "",
                        NonForestDataUrl = ""
                    }
                }) ;
            return new ClimateTraceDownloader(
                sampleOptions, 
                mockFileUnzipper.Object, 
                mockFileDownloader.Object
                );
        }

        [TestMethod]
        public void ClimateTraceDownloader_ForestDataPathSetAfterCreation()
        {
            var _cliamteTraceDownloader = CreateClimateTraceDownloader();
            Assert.IsNotNull(_cliamteTraceDownloader);
        }
    }
}