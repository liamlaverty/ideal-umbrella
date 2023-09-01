using IU.ClimateTrace.Data.Models.ClimateTraceDbModels;
using System.Diagnostics.CodeAnalysis;

namespace IU.ClimateTrace.Data.Repositories.Interface
{
    public class PagedResult<T>
    {
        [SetsRequiredMembers]
        public PagedResult(IEnumerable<T> results, int pageNumber, int pageSize, long pageCount, long totalRecords)
        {
            Results = results;
            PageNumber = pageNumber;
            PageSize = pageSize;
            PageCount = pageCount;
            TotalRecords = totalRecords;
        }
        public required IEnumerable<T> Results { get; set; }
        public required int PageNumber { get; set; }
        public required int PageSize { get; set; }
        public required long PageCount { get; set; }
        public required long TotalRecords { get; set; }
    }

    public interface IRepository<T> where T : class, IEntity
    {
        Task<bool> Exists(T entity);
        Task<PagedResult<T>> GetPagedAsync(int pageNumber = 0, int pageSize = 1000);
        Task<T> GetAsync(int id);
        Task DeleteAsync(int id);
        Task AddAsync(T entity);
        Task<T> Update(T entity);
    }
}
