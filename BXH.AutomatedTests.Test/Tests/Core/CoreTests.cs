using BXH.AutomatedTests.Api;
using NUnit.Framework;

namespace BXH.AutomatedTests.Test.Tests.Core
{
    [TestFixture]
    public class CoreTests
    {
        private static readonly TestHelper TestConfigs = new TestHelper("CORE");
        private readonly CoreApiTests _coreTests = new CoreApiTests(TestConfigs);

        [Test]
        public void PostAdvancedShipReturnsOk()
        {
            var result = _coreTests.ShipNotices();

            Assert.That(result, Is.Not.Null);
            StringAssert.Contains("SUCCESS", result);
        }

        [Test]
        public void PostBulkShipReturnsOk()
        {
            var result = _coreTests.BulkShipStatus();

            Assert.That(result, Is.Not.Null);
            StringAssert.Contains("SUCCESS", result);
        }

        [Test]
        public void PostBlendingReturnsOk()
        {
            var result = _coreTests.PostBlending();

            Assert.That(result, Is.Not.Null);
            StringAssert.Contains("SUCCESS", result);
        }


    }
}