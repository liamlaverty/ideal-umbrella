using IU.ClimateTrace.Data.Models.ClimateTraceDbModels;
using IU.ClimateTrace.Data.Repositories.Interface;
using IU.ClimateTrace.Importer.Web.API.Models.Dtos;
using IU.ClimateTrace.Importer.Web.API.Models.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace IU.ClimateTrace.Importer.Web.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AssetEmissionController : ControllerBase
    {
        private readonly ILogger<AssetEmissionController> _logger;
        private readonly IRepository<AssetEmission> _assetEmissionRepository;

        public AssetEmissionController(ILogger<AssetEmissionController> logger,
            IRepository<AssetEmission> assetEmissionRepository)
        {
            _assetEmissionRepository = assetEmissionRepository;
            _logger = logger;
        }

        [HttpGet(Name = "GetAssetEmissionsPaged")]
        [ProducesResponseType(typeof(PagedResultDto<AssetEmissionDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PagedResultDto<AssetEmissionDto>>> GetPaged(
            int pageNumber = 0, int pageSize = 50)
        {
            try
            {
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
                var result =await _assetEmissionRepository.GetPagedAsync(pageNumber, pageSize);


                return Ok(Mappers.MapFromPagedResult(result));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to {nameof(GetPaged)}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet(Name = "GetAssetEmissionsById")]
        [ProducesResponseType(typeof(AssetEmissionDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AssetEmissionDto>> GetById(int Id = 0)
        {
            try
            {
                if (Id < 0)
                {
                    _logger.LogInformation($"{nameof(Id)} had an invalid value ({Id})");
                    return BadRequest($"{nameof(Id)} cannot be negative");
                }
                AssetEmission result = (await _assetEmissionRepository.GetAsync(Id));
                return Ok(Mappers.MapFromAssetEmission(result));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to {nameof(GetPaged)}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
