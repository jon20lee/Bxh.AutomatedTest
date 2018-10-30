using System;
using System.Net.Mime;
using BXH.AutomatedTests.Api;
using BXH.AutomatedTests.Api.Apigee;
using BXH.AutomatedTests.Configs;
using NUnit.Framework;

namespace BXH.AutomatedTests.Test
{
    [TestFixture]
    public class ApigeeTests
    {
        private static readonly TestHelper ApigeeTestConfigs = new TestHelper("Apigee");
        private readonly ApigeeProxyTests _apigeeTests = new ApigeeProxyTests(ApigeeTestConfigs);

        [Test]
        public void PostAdvancedShipReturnsOk()
        {
            var result = _apigeeTests.ShipNotices();

            Assert.That(result, Is.Not.Null);
            StringAssert.Contains("SUCCESS", result);
        }

        [Test]
        public void PostBulkShipStatusReturnsOk()
        {
            var result = _apigeeTests.BulkShipStatus();

            Assert.That(result, Is.Not.Null);
            StringAssert.Contains("SUCCESS", result);
        }
    }
}
