namespace IU.ClimateTrace.Downloader
{
    class Program
    {
        static void Main(string[] args)
        {
            var startup = new Startup();

            Console.WriteLine($"Starting downloader. Result:");
            
            var result = startup.downloader.DownloadData();


            Console.WriteLine("Complete, press any key to exit");
            Console.Read();
        }
    }
}