using CsvHelper;
using IdealUmbrella.DataConnector.Models.CsvModels;
using System.Globalization;

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
            var countryCsvFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Assets/CsvData/all-iso-3166-countries.csv");

            using (var reader = new StreamReader(countryCsvFilePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<CsvCountryDto>().ToList();
                return records;
            }        
        }

    }
}