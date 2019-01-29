using BXH.AutomatedTests.Api;
using BXH.AutomatedTests.Api.Inner;
using NUnit.Framework;

namespace BXH.AutomatedTests.Test.Tests.Inner
{
    [TestFixture]
    public class InnerTests
    {
        private static readonly TestHelper TestConfigs = new TestHelper();
        private readonly InnerApiTests _innerTests = new InnerApiTests(TestConfigs);

        [Test]
        public void PostAdvancedShipReturnsOk()
        {
            var result = _innerTests.ShipNotices("HappyPath");

            Assert.That(result, Is.Not.Null);
            StringAssert.Contains("SUCCESS", result);
        }

        [Test]
        public void PostBulkShipReturnsOk()
        {
            var result = _innerTests.BulkShipStatus("HappyPath");

            Assert.That(result, Is.Not.Null);
            StringAssert.Contains("SUCCESS", result);
        }
        [Test]

        public void PostBulkShipStatusSendToAddress()
        {
            var result = _innerTests.BulkShipStatus("ToAddressEmailing");

            Assert.That(result, Is.Not.Null);
            StringAssert.Contains("SUCCESS", result);
        }

        [Test]
        public void PostBlendingReturnsOk()
        {
            var result = _innerTests.PostBlending("HappyPath");

            Assert.That(result, Is.Not.Null);
            StringAssert.Contains("SUCCESS", result);
        }

        [Test]
        public void GetInventory()
        {
            var result = _innerTests.GetInventory("HappyPath");

            Assert.That(result, Is.Not.Null);
            StringAssert.Contains("SUCCESS", result);
        }

        [Test]
        public void GetInventoryWithDate()
        {
            var result = _innerTests.GetInventory("DateParameter");

            Assert.That(result, Is.Not.Null);
            StringAssert.Contains("SUCCESS", result);
        }

        //Get Delivery Confirmations ... Currently Stubbed
        [Test]
        public void GetDelieveryConfirmations()
        {
            var result = _innerTests.GetDeliveryConfirmations("HappyPath");

            Assert.That(result, Is.Not.Null);
            StringAssert.Contains("SUCCESS", result);
        }
    }

}