using CsvHelper;
using CsvHelper.Configuration.Attributes;
using IdealUmbrella.DataConnector.Models.CsvModels;
using System.Diagnostics.CodeAnalysis;
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
            var countryGeolocationsCsvFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Assets/CsvData/google-countries-geolocation.csv");
            List<CsvCountryDto> countryRecords = new List<CsvCountryDto>();

            using (var reader = new StreamReader(countryCsvFilePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                countryRecords = csv.GetRecords<CsvCountryDto>().ToList();
            }

            using (var geolocationReader = new StreamReader(countryGeolocationsCsvFilePath))
            using (var csv = new CsvReader(geolocationReader, CultureInfo.InvariantCulture))
            {
                var countryGeolocationRecords = csv.GetRecords<CsvCountryGeolocationDto>().ToList();
                foreach (var record in countryRecords)
                {
                    
                    var pairedCountry = countryGeolocationRecords.FirstOrDefault(c => c.Country == record.Alpha2);
                    if (pairedCountry != null)
                    {
                        record.LatitudeAvg = pairedCountry.Latitude;
                        record.LongitudeAvg = pairedCountry.Longitude;
                    }
                    else
                    {
                        throw new Exception($"unpaired country {record.Alpha2}");
                    }
                    
                }

            }
            return countryRecords;

        }

    }

    


}