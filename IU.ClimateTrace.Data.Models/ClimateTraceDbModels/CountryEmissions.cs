namespace IU.ClimateTrace.Data.Models.ClimateTraceDbModels
{
    public class CountryEmissions : TrackedDataEntity
    {
        public string id { get; set; }
        public string iso3_country { get; set; }
        public string start_time { get; set; }
        public string end_time { get; set; }
        public string original_inventory_sector { get; set; }
        public string gas { get; set; }
        public int emissions_quantity { get; set; }
        public string emissions_quantity_units { get; set; }
        public string temporal_granularity { get; set; }
        public string created_date { get; set; }
        public string modified_date { get; set; }
    }
}