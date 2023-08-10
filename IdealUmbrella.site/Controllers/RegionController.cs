using IdealUmbrella.site.Models.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
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


        public RegionController(IContentService contentService,
            IOptions<SiteContentIdConfig> siteContentIdConfig)
        {
            _contentService = contentService;
            _siteContentIdConfig = siteContentIdConfig;
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

            var allCountries = GetCountryCsv();
    
            return "OK";
        }

        private IEnumerable<CsvCountryDto> GetCountryCsv()
        {
            throw new NotImplementedException();
        }
    }

    public class CsvCountryDto
    {

    }

    public class RegionDto
    {

    }
}
