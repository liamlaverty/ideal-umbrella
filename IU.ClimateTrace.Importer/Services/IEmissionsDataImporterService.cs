namespace IU.ClimateTrace.Importer.Services
{
    public interface IEmissionsDataImporterService
    {
        Task UpdateCountryEmissionsFromCsvAsync();
    }
}