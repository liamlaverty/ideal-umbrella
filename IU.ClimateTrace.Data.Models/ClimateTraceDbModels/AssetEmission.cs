using NetTopologySuite.Geometries;

namespace IU.ClimateTrace.Data.Models.ClimateTraceDbModels
{

   

    public class AssetEmission : TrackedDataEntity, IEntity
    {
        public AssetEmission(
                int asset_id,
                string iso3_country,
                string original_inventory_sector,
                DateTime start_time,
                DateTime end_time,
                string temporal_granularity,
                string gas,
                decimal? emissions_quantity,
                decimal? emissions_factor,
                string emissions_factor_units,
                decimal? capacity,
                string capacity_units,
                decimal? capacity_factor,
                decimal? activity,
                string activity_units,
                string origin_source,
                DateTime? source_created_date,
                DateTime? source_modified_date,
                DateTime created_date,
                DateTime? modified_date,
                string asset_name,
                string asset_type,
                Geometry st_astext) 
        {
            AssetId = asset_id;
            Iso3Country = iso3_country;
            OriginalInventorySector = original_inventory_sector;
            StartTime = start_time;
            EndTime = end_time;
            TemporalGranularity = temporal_granularity;
            Gas = gas;
            EmissionsQuantity = emissions_quantity;
            EmissionsFactor = emissions_factor;
            EmissionsFactorUnits = emissions_factor_units;
            Capacity = capacity;
            CapacityUnits = capacity_units;
            CapacityFactor = capacity_factor;
            Activity = activity;
            ActivityUnits = activity_units;
            CreatedDate = created_date;
            ModifiedDate = modified_date;
            OriginSource = origin_source;
            SourceCreatedDate = source_created_date;
            SourceModifiedDate = source_modified_date;
            AssetName = asset_name;
            AssetType = asset_type;
            StAstext = st_astext;
        }

        public int AssetId { get; set; }
        public string Iso3Country { get; set; }
        public string OriginalInventorySector { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string TemporalGranularity { get; set; }

        /// <summary>
        /// Possible values: 
        /// 
        /// ch4
        /// co2
        /// co2e_100yr
        /// co2e_20yr
        /// n2o
        /// </summary>
        public string Gas { get; set; }
        public decimal? EmissionsQuantity { get; set; }
        public decimal? EmissionsFactor { get; set; }
        public string EmissionsFactorUnits { get; set; }
        public decimal? Capacity { get; set; }
        public string CapacityUnits { get; set; }
        public decimal? CapacityFactor { get; set; }
        public decimal? Activity { get; set; }
        public string ActivityUnits { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? SourceCreatedDate { get; set; }
        public DateTime? SourceModifiedDate { get; set; }
        public string AssetName { get; set; }
        public string AssetType { get; set; }
        public string OriginSource { get; set; }
        public Geometry StAstext { get; set; }
    }
}