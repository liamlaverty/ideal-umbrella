using IdealUmbrella.DataConnector.CountryData;
using IdealUmbrella.DataConnector.Models.CsvModels;
using IdealUmbrella.site.Helpers.PropertyTypeHelpers;
using IdealUmbrella.site.Models.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace IdealUmbrella.site.Services.ContentServices.Impl
{

    public interface IRegionContentService
    {
        bool UpdateRegionsFromCsvFile();

        bool DeleteAllRegions();
    }



    public class RegionContentService : IRegionContentService
    {
        private readonly IContentService _contentService;
        private readonly ICountryDataCsvService _countryDataCsv;


        public RegionContentService(IContentService contentService,
            ICountryDataCsvService countryDataCsv)
        {
            _contentService = contentService;
            _countryDataCsv = countryDataCsv;
        }

        public bool DeleteAllRegions()
        {
            IContent rootContent = _contentService.GetRootContent().First();
            IEnumerable<IContent> rootChildren = _contentService.GetPagedChildren(rootContent.Id, pageIndex: 0, pageSize: 10, out _);
            var regionCollectionDoc = rootChildren.First(c => c.ContentType.Alias == RegionCollection.ModelTypeAlias);

            var pages = _contentService.GetPagedChildren(regionCollectionDoc.Id, 0, 1000, out _);
            foreach (var page in pages)
            {
                _contentService.Delete(page);
            }
            _contentService.EmptyRecycleBin();

            return true;
        }

        /// <summary>
        /// Opens the countries csv file, and upserts the countries inside into the 
        /// child nodes beneath the Region collection
        /// </summary>
        /// <returns></returns>
        public bool UpdateRegionsFromCsvFile()
        {
            IContent rootContent = _contentService.GetRootContent().First();
            IEnumerable<IContent> rootChildren = _contentService.GetPagedChildren(rootContent.Id, pageIndex: 0, pageSize: 10, out _);
            var regionCollectionDoc = rootChildren.First(c => c.ContentType.Alias == RegionCollection.ModelTypeAlias);

            var allCountries = _countryDataCsv.GetCountries();
            foreach (var country in allCountries)
            {
                AddOrUpdateCountry(regionCollectionDoc, country);
            }
            return true;
        }



        /// <summary>
        /// Upserts a country document 
        /// 
        /// 
        /// Checks if a country already exists, if so, overwrites its data with the CSV's data
        /// otherwise, creates a new country document, and inserts the CSV data into that
        /// </summary>
        /// <param name="parentDoc">The parent <see cref="RegionCollection"/> document this Country should be inserted into</param>
        /// <param name="country">The country to be upserted</param>
        /// <exception cref="ArgumentNullException">A parameter was null</exception>
        /// <exception cref="InvalidDocumentTypeException">The parent document type was of an invalid type</exception>
        /// <exception cref="NullReferenceException"></exception>
        private void AddOrUpdateCountry(IContent parentDoc, CsvCountryDto country)
        {
            if (parentDoc is null)
            {
                throw new ArgumentNullException(nameof(parentDoc));
            }
            if (parentDoc.ContentType.Alias != RegionCollection.ModelTypeAlias)
            {
                throw new InvalidDocumentTypeException($"Parent document must be of type {RegionCollection.ModelTypeAlias}");
            }
            if (country is null)
            {
                throw new ArgumentNullException(nameof(country));
            }

            int pageSize = 100;
            _contentService.GetPagedChildren(parentDoc.Id, 0, 1, out long countOfChildren);
            var countOfPages = Math.Ceiling((decimal)countOfChildren / pageSize);

            IContent regionToUpsertAsContent = null;

            bool pageExists = false;
            while (pageExists == false)
            {
                // page through the results, 
                for (int pageIndex = 0; pageIndex < countOfPages; pageIndex++)
                {
                    var pages = _contentService.GetPagedChildren(parentDoc.Id, pageIndex, pageSize, out _);
                    if (pages.Any(c => c.Name == country.Name))
                    {
                        pageExists = true;
                        regionToUpsertAsContent = pages.First(c => c.Name == country.Name);
                        break;
                    }
                }
                break;
            }
            if (!pageExists)
            {
                // If we've made it to the end of the loop, there's no content item with that name inthe paged results
                // so we need to create one
                regionToUpsertAsContent = _contentService.Create(country.Name, parentDoc.Id, RegionSingle.ModelTypeAlias);
            }

            if (regionToUpsertAsContent == null)
            {
                // verify that the region item was either found in the while loop, 
                // or is newly created
                throw new NullReferenceException(nameof(regionToUpsertAsContent));
            }

            regionToUpsertAsContent.SetValue("countryName", country.Name);
            regionToUpsertAsContent.SetValue("pageTitle", $"{country.Name}'s CO2e Emissions");
            regionToUpsertAsContent.SetValue("SEODescription", $"Detailed CO2e Emissions for '{country.Name}'.");
            regionToUpsertAsContent.SetValue("countryAlpha2", country.Alpha2);
            regionToUpsertAsContent.SetValue("countryAlpha3", country.Alpha3);
            regionToUpsertAsContent.SetValue("countryCode", country.CountryCode);
            regionToUpsertAsContent.SetValue("Regions", RepeatableTextStringHelper.FormatRepeatableTextStringProperty(new List<string>
            {
                country.Region,
                country.IntermediateRegion,
                country.SubRegion
            }));

            regionToUpsertAsContent.SetValue("placeName", country.Name);
            regionToUpsertAsContent.SetValue("latitude", country.LatitudeAvg);
            regionToUpsertAsContent.SetValue("longitude", country.LongitudeAvg);
            regionToUpsertAsContent.SetValue("mapZoom", 5);


            var publishResult = _contentService.SaveAndPublish(regionToUpsertAsContent);
            if (!publishResult.Success)
            {
                throw new Exception("publish result was not success");
            }
        }
    }
}
