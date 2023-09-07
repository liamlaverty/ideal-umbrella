using System.Diagnostics.CodeAnalysis;

namespace IU.ClimateTrace.Importer.Web.API.Models.Dtos
{
    public class CountryEmissionDto 
    {

        [SetsRequiredMembers]
        public CountryEmissionDto(int id, string iso3Country, DateTime startTime, DateTime endTime, string originalInventorySector, string gas, decimal emissionsQuantity, string emissionsQuantityUnits, string temporalGranularity, DateTime createdDate, DateTime modifiedDate, DateTime? sourceModifiedDate, DateTime? sourceCreatedDate, string originSource)
        {
            Id = id;
            Iso3Country = iso3Country;
            StartTime = startTime;
            EndTime = endTime;
            OriginalInventorySector = originalInventorySector;
            Gas = gas;
            EmissionsQuantity = emissionsQuantity;
            EmissionsQuantityUnits = emissionsQuantityUnits;
            TemporalGranularity = temporalGranularity;
            CreatedDate = createdDate;
            ModifiedDate = modifiedDate;
            SourceModifiedDate = sourceModifiedDate;
            SourceCreatedDate = sourceCreatedDate;
            OriginSource = originSource;
        }

        public required int Id { get; set; }
        public required string Iso3Country { get; set; }
        public required DateTime StartTime { get; set; }
        public required DateTime EndTime { get; set; }
        public required string OriginalInventorySector { get; set; }
        public required string Gas { get; set; }
        public required decimal EmissionsQuantity { get; set; }
        public required string EmissionsQuantityUnits { get; set; }
        public required string TemporalGranularity { get; set; }
        public required DateTime CreatedDate { get; set; }
        public required DateTime ModifiedDate { get; set; }
        public required DateTime? SourceModifiedDate { get; set; }
        public required DateTime? SourceCreatedDate { get; set; }
        public required string OriginSource { get; set; }
    }
}
