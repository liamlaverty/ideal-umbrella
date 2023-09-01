using IU.ClimateTrace.Data.Models.ClimateTraceDbModels;

namespace IU.ClimateTrace.Data.Repositories.Interface
{
    public interface IRepository<T> where T : class, IEntity
    {
        Task<bool> Exists(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(int id);
        Task AddAsync(T entity);
        Task<T> Update(T entity);
        Task<T> DeleteAsync(int id);
    }
}
