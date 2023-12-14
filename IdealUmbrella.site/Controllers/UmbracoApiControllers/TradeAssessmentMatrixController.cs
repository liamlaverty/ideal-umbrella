using IdealUmbrella.TradeMatrix.Models;
using IdealUmbrella.TradeMatrix.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Web.Common.Controllers;

namespace IdealUmbrella.site.Controllers.UmbracoApiControllers
{
    [Route("TradeAssessmentMatrix/[action]")]
    public class TradeAssessmentMatrixController : UmbracoApiController
    {
        private readonly ITradeMatrixService _tradeMatrixService;

        public TradeAssessmentMatrixController(ITradeMatrixService tradeMatrixService ) 
        {
            _tradeMatrixService = tradeMatrixService;
        }

        /// <summary>
        /// Gets a sample TradeMatrix result
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public TradeAssessmentMatrix Get()
        {
            return _tradeMatrixService.Get();
        }


        /// <summary>
        /// Generates a TradeMatrix result json object from a given matrix dataset
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public TradeAssessmentMatrix Generate([FromBody] TradeMatrixGenerateRequestDto data)
        {
            return _tradeMatrixService.Generate(data);
        }
    }    
}
