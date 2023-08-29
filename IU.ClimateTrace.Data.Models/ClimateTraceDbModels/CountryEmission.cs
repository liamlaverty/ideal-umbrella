namespace IU.ClimateTrace.Data.Models.ClimateTraceDbModels
{
    public class CountryEmission : TrackedDataEntity, IEntity
    {

//        System.Int32 id,
//System.String iso3_country,
//System.DateTime start_time,
//System.DateTime end_time,
//System.String original_inventory_sector,
//System.String gas,
//System.Int64 emissions_quantity,
//System.String emissions_quantity_units,
//System.String temporal_granularity,
//System.String origin_source,
//System.DateTime source_created_date,
//System.DateTime source_modified_date,
//System.DateTime created_date,
//System.DateTime modified_date


        public CountryEmission(
            int id,
            string iso3_country,
            DateTime start_time,
            DateTime end_time,
            string original_inventory_sector,
            string gas,
            long emissions_quantity,
            string emissions_quantity_units,
            string temporal_granularity,
            string origin_source,
            DateTime source_created_date,
            DateTime source_modified_date,
            DateTime created_date,
            DateTime modified_date
            )
        {

            Id = id;
            Iso3Country = iso3_country;
            StartTime = start_time;
            EndTime = end_time;
            OriginalInventorySector = original_inventory_sector;
            Gas = gas;
            EmissionsQuantity = emissions_quantity;
            EmissionsQuantityUnits = emissions_quantity_units;
            TemporalGranularity = temporal_granularity;
            CreatedDate = created_date;
            ModifiedDate = modified_date;
            OriginSource = origin_source;
            SourceCreatedDate = source_created_date;
            SourceModifiedDate = source_modified_date;
        }
        public int Id { get; set; }
        public string Iso3Country { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string OriginalInventorySector { get; set; }
        public string Gas { get; set; }
        public long EmissionsQuantity { get; set; }
        public string EmissionsQuantityUnits { get; set; }
        public string TemporalGranularity { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime SourceModifiedDate { get; set; }
        public DateTime SourceCreatedDate { get; set; }
        public string OriginSource { get; set; }
    }
}