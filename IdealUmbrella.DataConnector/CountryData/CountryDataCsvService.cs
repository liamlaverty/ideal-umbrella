using IdealUmbrella.DataConnector.Models.CsvModels;

namespace IdealUmbrella.DataConnector.CountryData
{
    public interface ICountryDataCsvService
    {
        IEnumerable<CsvCountryDto> GetCountries();
    }



    /// <summary>
    /// provides a service to get data from the CSV file of all countries
    /// </summary>
    public class CountryDataCsvService : ICountryDataCsvService
    {
        public CountryDataCsvService() { }

        public IEnumerable<CsvCountryDto> GetCountries() 
        {
            return new
                List<CsvCountryDto>
            {
                new CsvCountryDto
                {
                    Name = "Test"
                }
            };
        
        }

    }
}