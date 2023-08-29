namespace IU.ClimateTrace.Data.Models.ClimateTraceDbModels
{
    public class AssetOwnership : TrackedDataEntity
    {
        public string asset_id { get; set; }
        public string asset_name { get; set; }
        public string owner_name { get; set; }
        public string owner_classification { get; set; }
        public string percentage_of_ownership { get; set; }
        public string owner_direct_parent { get; set; }
        public string owner_grouping { get; set; }
        public string operator_name { get; set; }
        public string percentage_of_operation { get; set; }
        public string data_source { get; set; }
        public string url { get; set; }
        public string recency { get; set; }
        public string created_date { get; set; }
        public string original_inventory_sector { get; set; }
    }
}