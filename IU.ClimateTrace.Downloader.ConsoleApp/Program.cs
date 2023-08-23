namespace IU.ClimateTrace.Downloader
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine($"Starting downloader");

            var startup = new Startup();

            var result = await startup.downloader.DownloadData();


            Console.WriteLine("Complete, press any key to exit");
            Console.Read();
        }
    }
}