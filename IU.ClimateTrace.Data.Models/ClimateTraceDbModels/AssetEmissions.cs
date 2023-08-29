namespace IU.ClimateTrace.Data.Models.ClimateTraceDbModels
{
    public class AssetEmissions : TrackedDataEntity
    {
        public string asset_id { get; set; }
        public string iso3_country { get; set; }
        public string original_inventory_sector { get; set; }
        public string start_time { get; set; }
        public string end_time { get; set; }
        public string temporal_granularity { get; set; }
        public string gas { get; set; }
        public string emissions_quantity { get; set; }
        public string emissions_factor { get; set; }
        public string emissions_factor_units { get; set; }
        public string capacity { get; set; }
        public string capacity_units { get; set; }
        public string capacity_factor { get; set; }
        public string activity { get; set; }
        public string activity_units { get; set; }
        public string created_date { get; set; }
        public string modified_date { get; set; }
        public string asset_name { get; set; }
        public string asset_type { get; set; }
        public string st_astext { get; set; }
    }
}