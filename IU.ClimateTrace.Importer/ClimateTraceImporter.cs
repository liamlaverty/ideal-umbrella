using Npgsql;

namespace IU.ClimateTrace.Importer
{
    public interface IClimateTraceImporter
    {
        Task ImportData();
    }
    public class ClimateTraceImporter : IClimateTraceImporter
    {

        public ClimateTraceImporter() 
        {
            Console.WriteLine($"starting {nameof(ClimateTraceImporter)}");
        }

        public async Task ImportData()
        {
            var conn = new NpgsqlConnection(
                connectionString:
                ""
                );
            conn.Open();
            using var cmd = new NpgsqlCommand();
            cmd.Connection = conn;


            cmd.CommandText = "SELECT * FROM country_emissions LIMIT 500";

            NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();
            var result = new List<string>();
            while (await reader.ReadAsync())
            {
                result.Add((string)reader["id"]?.ToString());

            }
            foreach (var item in result)
            {
                Console.WriteLine(item);
            }
        }
    }



}