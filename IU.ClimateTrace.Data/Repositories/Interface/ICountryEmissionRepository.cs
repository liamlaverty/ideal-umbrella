using IU.ClimateTrace.Common.DataFilters;
using IU.ClimateTrace.Data.Models.ClimateTraceDbModels;

namespace IU.ClimateTrace.Data.Repositories.Interface
{
    public interface ICountryEmissionRepository : IRepository<CountryEmission>
    {
        Task<PagedResult<CountryEmission>> GetPagedAsync(
            string isoCountryCode,
            string gasName,
            DateTime startDate,
            DateTime endDate,
            OrderByDirection orderByDirection,
            CountryEmissionsOrderByCol orderByColumn,
            int pageNumber = 0,
            int pageSize = 50);
    }
}
