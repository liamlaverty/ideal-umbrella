using IdealUmbrella.DataConnector.CountryData;
using IdealUmbrella.DataConnector.Models.CsvModels;
using IdealUmbrella.site.Helpers.PropertyTypeHelpers;
using IdealUmbrella.site.Models.Config;
using IdealUmbrella.site.Models.Exceptions;
using IdealUmbrella.site.Services.ContentServices.Impl;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Web.Common.Authorization;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace IdealUmbrella.site.Controllers
{

    public class RegionContentImporterController : UmbracoApiController
    {
        private readonly IContentService _contentService;
        private readonly ICountryDataCsvService _countryDataCsv;
        private readonly IRegionContentService _regionContentService;


        public RegionContentImporterController(IContentService contentService,
            ICountryDataCsvService countryDataCsv,
            IRegionContentService regionContentService)
        {
            _contentService = contentService;
            _countryDataCsv = countryDataCsv;
            _regionContentService = regionContentService;
        }

#if !DEBUG 
        [Authorize(Policy = AuthorizationPolicies.BackOfficeAccess)]
#endif
        [HttpPost]
        public string UpdateRegions()
        {
            _regionContentService.UpdateRegionsFromCsvFile();
            return "OK";
        }

#if !DEBUG
        [Authorize(Policy = AuthorizationPolicies.BackOfficeAccess)]
#endif
        [HttpDelete]
        public string DeleteRegions()
        {
            _regionContentService.DeleteAllRegions();
            return "OK";
        }

    }
}
