namespace IU.ClimateTrace.Importer.ConsoleApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            ClimateTraceImporter importer = new ClimateTraceImporter();
            await importer.ImportData();
        }
    }
}