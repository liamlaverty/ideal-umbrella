using IU.ClimateTrace.Data.Models.ClimateTraceDbModels;
using IU.ClimateTrace.Data.Repositories.Interface;
using IU.ClimateTrace.Importer.Web.API.Models.Dtos;

namespace IU.ClimateTrace.Importer.Web.API.Models.Mappers
{
    internal static class Mappers
    {
        public static IEnumerable<CountryEmissionDto> MapFromCountryEmission(IEnumerable<CountryEmission> countryEmissions)
        {
            var results = new List<CountryEmissionDto>();
            for (var i = 0; i < countryEmissions.Count(); i++)
            {
                results.Add(MapFromCountryEmission(countryEmissions.ElementAt(i)));
            }
            return results;
        }
        public static CountryEmissionDto MapFromCountryEmission(CountryEmission countryEmission)
        {
            return new CountryEmissionDto(
                countryEmission.Id,
                countryEmission.Iso3Country,
                countryEmission.StartTime,
                countryEmission.EndTime,
                countryEmission.OriginalInventorySector,
                countryEmission.Gas,
                countryEmission.EmissionsQuantity,
                countryEmission.EmissionsQuantityUnits,
                countryEmission.TemporalGranularity,
                countryEmission.CreatedDate,
                countryEmission.ModifiedDate,
                countryEmission.SourceModifiedDate,
                countryEmission.SourceCreatedDate,
                countryEmission.OriginSource
            );
        }


        public static IEnumerable<AssetEmissionDto> MapFromAssetEmission(IEnumerable<AssetEmission> assetEmission)
        {
            var results = new List<AssetEmissionDto>();
            for (var i = 0; i < assetEmission.Count(); i++)
            {
                results.Add(MapFromAssetEmission(assetEmission.ElementAt(i)));
            }
            return results;
        }


        public static PagedResultDto<CountryEmissionDto> MapFromPagedResult(PagedResult<CountryEmission> paged)
        {
            return new PagedResultDto<CountryEmissionDto>
            {
                Results = MapFromCountryEmission(paged.Results),
                PageCount = paged.PageCount,
                PageNumber = paged.PageNumber,
                PageSize = paged.PageSize,
                TotalRecords = paged.TotalRecords,
            };
        }

        public static PagedResultDto<AssetEmissionDto> MapFromPagedResult(PagedResult<AssetEmission> paged)
        {
            return new PagedResultDto<AssetEmissionDto>
            {
                Results = MapFromAssetEmission(paged.Results),
                PageCount = paged.PageCount,
                PageNumber = paged.PageNumber,
                PageSize = paged.PageSize,
                TotalRecords = paged.TotalRecords,
            };
        }


        public static AssetEmissionDto MapFromAssetEmission(AssetEmission assetEmission)
        {
            return new AssetEmissionDto(
                assetEmission.Id,
                assetEmission.Iso3Country,
                assetEmission.OriginalInventorySector,
                assetEmission.StartTime,
                assetEmission.EndTime,
                assetEmission.TemporalGranularity,
                assetEmission.Gas,
                assetEmission.EmissionsQuantity,
                assetEmission.EmissionsFactor,
                assetEmission.EmissionsFactorUnits,
                assetEmission.Capacity,
                assetEmission.CapacityUnits,
                assetEmission.CapacityFactor,
                assetEmission.Activity,
                assetEmission.ActivityUnits,
                assetEmission.origin_source,
                assetEmission.SourceCreatedDate,
                assetEmission.SourceModifiedDate,
                assetEmission.CreatedDate,
                assetEmission.ModifiedDate,
                assetEmission.AssetName,
                assetEmission.AssetType,
                assetEmission.StAstext);
        }
    }
}