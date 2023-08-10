using IdealUmbrella.DataConnector.CountryData;
using IdealUmbrella.DataConnector.Models.CsvModels;
using IdealUmbrella.site.Helpers.PropertyTypeHelpers;
using IdealUmbrella.site.Models.Config;
using IdealUmbrella.site.Models.Exceptions;
using J2N.Text;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Drawing;
using System.Text;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Infrastructure.Persistence.Querying;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace IdealUmbrella.site.Controllers
{
    public class RegionController : UmbracoApiController
    {
        private IContentService _contentService;
        private readonly IOptions<SiteContentIdConfig> _siteContentIdConfig;
        private readonly ICountryDataCsvService _countryDataCsv;


        public RegionController(IContentService contentService,
            IOptions<SiteContentIdConfig> siteContentIdConfig,
            ICountryDataCsvService countryDataCsv)
        {
            _contentService = contentService;
            _siteContentIdConfig = siteContentIdConfig;
            _countryDataCsv = countryDataCsv;
        }


        [HttpGet] 
        public List<RegionDto> GetRegionCollection()
        {
            return new List<RegionDto> { new RegionDto() };
        }


        [HttpPost]
        public string UpdateRegions()
        {
            IContent rootContent = _contentService.GetRootContent().First();
            IEnumerable<IContent> rootChildren = _contentService.GetPagedChildren(rootContent.Id, pageIndex: 0, pageSize: 10, out _);
            var regionCollectionDoc = rootChildren.First(c => c.ContentType.Alias == RegionCollection.ModelTypeAlias);

            var allCountries = _countryDataCsv.GetCountries();
            foreach (var country in allCountries)
            {
                AddOrUpdateCountry(regionCollectionDoc, country);
            }
    
            return "OK" + allCountries.Count();
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

            _contentService.SaveAndPublish(regionToUpsertAsContent);
        }
    }

    public class RegionDto
    {

    }
}
