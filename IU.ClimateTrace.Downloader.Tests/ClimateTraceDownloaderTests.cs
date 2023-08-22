namespace IU.ClimateTrace.Downloader.Tests
{
    [TestClass]
    public class ClimateTraceDownloaderTests
    {

        private ClimateTraceDownloader CreateClimateTraceDownloader()
        {
            return new ClimateTraceDownloader();
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