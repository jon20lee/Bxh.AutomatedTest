﻿using BXH.AutomatedTests.Api;
using NUnit.Framework;

namespace BXH.AutomatedTests.Test.Tests.Core
{
    [TestFixture]
    public class CoreTests
    {
        private static readonly TestHelper TestConfigs = new TestHelper();
        private readonly CoreApiTests _coreTests = new CoreApiTests(TestConfigs);

        [Test]
        public void PostAdvancedShipReturnsOk()
        {
            var result = _coreTests.ShipNotices("HappyPath");

            Assert.That(result, Is.Not.Null);
            StringAssert.Contains("SUCCESS", result);
        }

        [Test]
        public void PostBulkShipReturnsOk()
        {
            var result = _coreTests.BulkShipStatus("HappyPath");

            Assert.That(result, Is.Not.Null);
            StringAssert.Contains("SUCCESS", result);
        }

        [Test]
        public void PostBulkShipStatusSendToAddress()
        {
            var result = _coreTests.BulkShipStatus("ToAddressEmailing");

            Assert.That(result, Is.Not.Null);
            StringAssert.Contains("SUCCESS", result);
        }

        [Test]
        public void PostBlendingReturnsOk()
        {
            var result = _coreTests.PostBlending("HappyPath");

            Assert.That(result, Is.Not.Null);
            StringAssert.Contains("SUCCESS", result);
        }

        [Test]
        public void GetInventory()
        {
            var result = _coreTests.GetInventory("HappyPath");

            Assert.That(result, Is.Not.Null);
            StringAssert.Contains("SUCCESS", result);
        }

        [Test]
        public void GetInventoryWithDate()
        {
            var result = _coreTests.GetInventory("DateParameter");

            Assert.That(result, Is.Not.Null);
            StringAssert.Contains("SUCCESS", result);
        }

        //Get Delivery Confirmations ... Currently Stubbed
        [Test]
        public void GetDelieveryConfirmations()
        {
            var result = _coreTests.GetDeliveryConfirmations("HappyPath");

            Assert.That(result, Is.Not.Null);
            StringAssert.Contains("SUCCESS", result);
        }

    }
}