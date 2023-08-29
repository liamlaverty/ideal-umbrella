using IU.ClimateTrace.Data.Context;
using IU.ClimateTrace.Data.Models.ClimateTraceDbModels;
using IU.ClimateTrace.Data.Repositories.Interface;

namespace IU.ClimateTrace.Data.Repositories
{
    public class CountryEmissionRepository : IRepository<CountryEmission>
    {
        private IPostgresContext context;


        public CountryEmissionRepository(IPostgresContext pgContext)
        {
            context = pgContext;
        }

        public Task<CountryEmission> AddAsync(CountryEmission entity)
        {
            throw new NotImplementedException();
        }

        public Task<CountryEmission> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CountryEmission>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CountryEmission> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<CountryEmission> Update(CountryEmission entity)
        {
            throw new NotImplementedException();
        }
    }
}
