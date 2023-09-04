using IU.ClimateTrace.Data.Models.ClimateTraceDbModels;
using IU.ClimateTrace.Data.Repositories;
using IU.ClimateTrace.Data.Repositories.Interface;
using IU.ClimateTrace.Importer.Web.API.Models.Dtos;
using IU.ClimateTrace.Importer.Web.API.Models.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace IU.ClimateTrace.Importer.Web.API.Controllers
{
    public enum OrderByDirection
    {
        ASC,
        DESC
    }
    public enum CountryEmissionsOrderByCol
    {
        startTime,
        endTime, 
        isoCountry,
        emissionsQuantity
    }

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CountryEmissionsController : ControllerBase
    {
        private readonly ILogger<CountryEmissionsController> _logger;
        private readonly IRepository<CountryEmission> _countryEmissionRepository;
        public CountryEmissionsController(
            ILogger<CountryEmissionsController> logger,
            IRepository<CountryEmission> countryEmissionRepository) 
        {
            _logger = logger;
            _countryEmissionRepository = countryEmissionRepository;
        }


        [HttpGet(Name = "GetCountryEmissionsPaged")]
        [ProducesResponseType(typeof(PagedResult<CountryEmissionDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PagedResult<CountryEmissionDto>>> GetPaged(
            //string isoCountryCode, string gasName, 
            //DateTime startDate, DateTime endDate,
            //OrderByDirection orderByDirection, CountryEmissionsOrderByCol orderByColumn,
            int pageNumber = 0, int pageSize = 50)
        {
            try
            {
                // TODO: implement 
                //string isoCountryCode, string gasName, 
                //DateTime startDate, DateTime endDate,
                //OrderByDirection orderByDirection, CountryEmissionsOrderByCol orderByColumn

                if (pageNumber < 0)
                {
                    _logger.LogInformation($"{nameof(pageNumber)} had an invalid value ({pageNumber})");
                    return BadRequest($"{nameof(pageNumber)} cannot be negative");
                }
                if (pageSize < 1)
                {
                    _logger.LogInformation($"{nameof(pageSize)} had an invalid value ({pageSize})");
                    return BadRequest($"{nameof(pageSize)} must be greater than 0");

                }
                var result = await _countryEmissionRepository.GetPagedAsync(pageNumber, pageSize);


                return Ok(Mappers.MapFromPagedResult(result));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to {nameof(GetPaged)}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet(Name = "GetCountryEmissionsById")]
        [ProducesResponseType(typeof(CountryEmissionDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CountryEmissionDto>> GetById(int Id = 0)
        {
            try
            {
                if (Id < 0)
                {
                    _logger.LogInformation($"{nameof(Id)} had an invalid value ({Id})");
                    return BadRequest($"{nameof(Id)} cannot be negative");
                }
                var result = await _countryEmissionRepository.GetAsync(Id);
                return Ok(Mappers.MapFromCountryEmission(result));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to {nameof(GetPaged)}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
