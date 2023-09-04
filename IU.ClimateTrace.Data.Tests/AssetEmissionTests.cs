using IU.ClimateTrace.Data.Models.ClimateTraceDbModels;

namespace IU.ClimateTrace.Data.Tests
{
    [TestClass]
    public class AssetEmissionTests
    {
        [TestMethod]
        public void AssetEmissionIsOfTypeTrackedDataEntity()
        {
            Assert.IsTrue(typeof(TrackedDataEntity).IsAssignableFrom(typeof(AssetEmission)));
        }

        [TestMethod]
        public void AssetEmissionIsOfTypeIEntity()
        {
            Assert.IsTrue(typeof(IEntity).IsAssignableFrom(typeof(AssetEmission)));
        }

        [TestMethod]
        public void AssetEmissionsOriginSourceIsClimateTrace()
        {
            var emission = new AssetEmission(
                1, "test_iso3_country", "test_original_inventory_sector", DateTime.UtcNow, DateTime.UtcNow,
                "test_temporal_granularity", "test_gas", 0, 0, "test_emissions_factor_units",
                0, "test_capacity_units", 0, 0, "test_activity_units", "test_origin_source", 
                DateTime.UtcNow, DateTime.UtcNow, DateTime.UtcNow, DateTime.UtcNow,
                "test_asset_name", "test_asset_type", null);

            Assert.AreEqual("climate_trace", emission.origin_source);
        }
    }
}