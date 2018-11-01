using BXH.AutomatedTests.Api;
using BXH.AutomatedTests.Api.Bxh;
using NUnit.Framework;

namespace BXH.AutomatedTests.Test.Tests.Bxh
{
    [TestFixture]
    public class BxhTests
    {
        private static readonly TestHelper TestConfigs = new TestHelper();
        private readonly BxhApiTests _bxhTests = new BxhApiTests(TestConfigs);

        [Test]
        public void PostReturnsForbiddenWithInvalidApiKey()
        {
            _bxhTests.useInvalidApiKey = true;
            var result = _bxhTests.ShipNotices("InvalidApiKey");
            _bxhTests.useInvalidApiKey = false;

            Assert.That(result, Is.Not.Null);
            StringAssert.Contains("SUCCESS", result);
        }

        [Test]
        public void PostAdvancedShipReturnsOk()
        {
            var result = _bxhTests.ShipNotices("HappyPath");

            Assert.That(result, Is.Not.Null);
            StringAssert.Contains("SUCCESS", result);
        }

        [Test]
        public void PostBulkShipReturnsOk()
        {
            var result = _bxhTests.BulkShipStatus("HappyPath");

            Assert.That(result, Is.Not.Null);
            StringAssert.Contains("SUCCESS", result);
        }

        [Test]
        public void PostBlendingReturnsOk()
        {
            var result = _bxhTests.PostBlending("HappyPath");

            Assert.That(result, Is.Not.Null);
            StringAssert.Contains("SUCCESS", result);
        }
    }
}