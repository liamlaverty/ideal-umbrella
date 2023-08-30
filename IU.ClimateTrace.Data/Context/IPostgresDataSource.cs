using Npgsql;

namespace IU.ClimateTrace.Data.Context
{
    public interface IPostgresDataSource
    {
        Task<NpgsqlDataSource> GetDataSourceAsync();
    }
}
