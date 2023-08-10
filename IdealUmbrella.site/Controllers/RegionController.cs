using IdealUmbrella.DataConnector.CountryData;
using IdealUmbrella.DataConnector.Models.CsvModels;
using IdealUmbrella.site.Models.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Drawing;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;
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
    
            return "OK";
        }
    }



    public class RegionDto
    {

    }
}
