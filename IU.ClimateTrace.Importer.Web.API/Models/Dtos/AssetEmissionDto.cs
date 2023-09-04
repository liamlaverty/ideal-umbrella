using NetTopologySuite.Geometries;

namespace IU.ClimateTrace.Importer.Web.API.Models.Dtos
{
    public class AssetEmissionDto
    {
        public AssetEmissionDto(int assetId,
            string iso3Country,
            string originalInventorySector,
            DateTime startTime,
            DateTime endTime,
            string temporalGranularity,
            string gas,
            decimal? emissionsQuantity,
            decimal? emissionsFactor,
            string emissionsFactorUnits,
            decimal? capacity,
            string capacityUnits,
            decimal? capacityFactor,
            decimal? activity,
            string activityUnits,
            string originSource,
            DateTime? sourceCreatedDate,
            DateTime? sourceModifiedDate,
            DateTime createdDate,
            DateTime? modifiedDate,
            string assetName,
            string assetType,
            Geometry stAstext)
        {
            AssetId = assetId;
            Iso3Country = iso3Country;
            OriginalInventorySector = originalInventorySector;
            StartTime = startTime;
            EndTime = endTime;
            TemporalGranularity = temporalGranularity;
            Gas = gas;
            EmissionsQuantity = emissionsQuantity;
            EmissionsFactor = emissionsFactor;
            EmissionsFactorUnits = emissionsFactorUnits;
            Capacity = capacity;
            CapacityUnits = capacityUnits;
            CapacityFactor = capacityFactor;
            Activity = activity;
            ActivityUnits = activityUnits;
            OriginSource = originSource;
            SourceCreatedDate = sourceCreatedDate;
            SourceModifiedDate = sourceModifiedDate;
            CreatedDate = createdDate;
            ModifiedDate = modifiedDate;
            AssetName = assetName;
            AssetType = assetType;
            StAstext = stAstext.ToString();
        }

        public int AssetId { get; }
        public string Iso3Country { get; }
        public string OriginalInventorySector { get; }
        public DateTime StartTime { get; }
        public DateTime EndTime { get; }
        public string TemporalGranularity { get; }
        public string Gas { get; }
        public decimal? EmissionsQuantity { get; }
        public decimal? EmissionsFactor { get; }
        public string EmissionsFactorUnits { get; }
        public decimal? Capacity { get; }
        public string CapacityUnits { get; }
        public decimal? CapacityFactor { get; }
        public decimal? Activity { get; }
        public string ActivityUnits { get; }
        public string OriginSource { get; }
        public DateTime? SourceCreatedDate { get; }
        public DateTime? SourceModifiedDate { get; }
        public DateTime CreatedDate { get; }
        public DateTime? ModifiedDate { get; }
        public string AssetName { get; }
        public string AssetType { get; }
        public string StAstext { get; }
    }
}
