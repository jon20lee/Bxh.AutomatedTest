﻿using BXH.AutomatedTests.Api;
using BXH.AutomatedTests.Api.Bxh;
using NUnit.Framework;

namespace BXH.AutomatedTests.Test.Tests.Bxh
{
    [TestFixture]
    public class BxhTests
    {
        private static readonly TestHelper TestConfigs = new TestHelper("BXH");
        private readonly BxhApiTests _bxhTests = new BxhApiTests(TestConfigs);

        [Test]
        public void PostAdvancedShipReturnsOk()
        {
            var result = _bxhTests.ShipNotices();

            Assert.That(result, Is.Not.Null);
            StringAssert.Contains("SUCCESS", result);
        }

        [Test]
        public void PostBulkShipReturnsOk()
        {
            var result = _bxhTests.BulkShipStatus();

            Assert.That(result, Is.Not.Null);
            StringAssert.Contains("SUCCESS", result);
        }

        [Test]
        public void PostBlendingReturnsOk()
        {
            var result = _bxhTests.PostBlending();

            Assert.That(result, Is.Not.Null);
            StringAssert.Contains("SUCCESS", result);
        }
    }
}