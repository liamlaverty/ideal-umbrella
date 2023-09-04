using IU.ClimateTrace.Data.Models.ClimateTraceDbModels;

namespace IU.ClimateTrace.Data.Tests
{
    [TestClass]
    public class CountryEmissionTests
    {
        [TestMethod]
        public void CountryEmissionIsOfTypeTrackedDataEntity()
        {
            Assert.IsTrue(typeof(TrackedDataEntity).IsAssignableFrom(typeof(CountryEmission)));
        }
        [TestMethod]
        public void CountryEmissionIsOfTypeIEntity()
        {
            Assert.IsTrue(typeof(IEntity).IsAssignableFrom(typeof(CountryEmission)));
        }
    }
}