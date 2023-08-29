namespace IU.ClimateTrace.Data.Models.ClimateTraceDbModels
{
    public class TrackedDataEntity
    {
        public string origin_source { get; set; } = "climate_trace";
        public DateTime imported_date { get; set; }
    }
}