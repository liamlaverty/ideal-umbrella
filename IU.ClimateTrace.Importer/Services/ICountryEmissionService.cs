namespace IU.ClimateTrace.Importer.Services
{
    public interface ICountryEmissionService
    {
        Task UpdateCountryEmissionsFromCsvAsync();
    }
}