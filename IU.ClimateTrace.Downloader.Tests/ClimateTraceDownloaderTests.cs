using IU.ClimateTrace.Downloader.Models.Config;
using Microsoft.Extensions.Options;

namespace IU.ClimateTrace.Downloader.Tests
{
    [TestClass]
    public class ClimateTraceDownloaderTests
    {

        private ClimateTraceDownloader CreateClimateTraceDownloader()
        {
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
            return new ClimateTraceDownloader(sampleOptions);
        }

        [TestMethod]
        public void ClimateTraceDownloader_ForestDataPathSetAfterCreation()
        {
            var _cliamteTraceDownloader = CreateClimateTraceDownloader();


            Assert.IsNotNull(_cliamteTraceDownloader);

        }

        //[TestMethod]
        //public void ClimateTraceDownloader_ForestDataPathSetAfterCreation()
        //{
        //    var _cliamteTraceDownloader = CreateClimateTraceDownloader();


        //    Assert.IsNotNull(_cliamteTraceDownloader.)

        //}
    }
}