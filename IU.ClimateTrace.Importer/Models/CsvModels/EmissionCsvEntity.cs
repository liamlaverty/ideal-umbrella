﻿using CsvHelper.Configuration.Attributes;

namespace IU.ClimateTrace.Importer.Models.CsvModels
{
    [Delimiter(",")]
    internal class EmissionCsvEntity
    {

        [Optional]
        [Name("asset_id")]
        public int AssetId { get; set; }

        [Optional]
        [Name("iso3_country")]
        public string Iso3Country { get; set; }

        [Optional]
        [Name("original_inventory_sector")]
        public string OriginalInventorySector { get; set; }

        [Optional]
        [Name("start_time")]
        public DateTime StartTime { get; set; }

        [Optional]
        [Name("end_time")]
        public DateTime EndTime { get; set; }

        [Optional]
        [Name("temporal_granularity")]
        public string TemporalGranularity { get; set; }

        [Optional]
        [Name("gas")]
        public string Gas { get; set; }

        [Optional]
        [Name("emissions_quantity")]
        public decimal? EmissionsQuantity { get; set; }

        [Optional]
        [Name("Lat_lon")]
        public string LatLon { get; set; }

        [Optional]
        [Name("emissions_quantity_units")]
        public string EmissionsQuantityUnits { get; set; }

        [Optional]
        [Name("emissions_factor")]
        public decimal? EmissionsFactor { get; set; }

        [Optional]
        [Name("emissions_factor_units")]
        public string EmissionsFactorUnits { get; set; }

        [Optional]
        [Name("capacity")]
        public decimal? Capacity { get; set; }

        [Optional]
        [Name("capacity_units")]
        public string CapacityUnits { get; set; }

        [Optional]
        [Name("capacity_factor")]
        public decimal? CapacityFactor { get; set; }

        [Optional]
        [Name("activity")]
        public decimal? Activity { get; set; }

        [Optional]
        [Name("activity_units")]
        public string ActivityUnits { get; set; }

        [Optional]
        [Name("created_date")]
        public DateTime CreatedDate { get; set; }

        [Optional]
        [Name("modified_date")]
        public DateTime? ModifiedDate { get; set; }

        [Optional]
        [Name("asset_name")]
        public string AssetName { get; set; }

        [Optional]
        [Name("asset_type")]
        public string AssetType { get; set; }

        [Optional]
        [Name("st_astext")]
        public string StAstex { get; set; }


    }
}
