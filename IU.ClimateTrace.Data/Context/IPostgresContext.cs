using Npgsql;

namespace IU.ClimateTrace.Data.Context
{
    public interface IPostgresContext
    {
        Task<NpgsqlDataSource> GetDataSourceAsync();
    }
}
