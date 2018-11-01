using System;
using System.Net.Mime;
using BXH.AutomatedTests.Api;
using BXH.AutomatedTests.Api.Apigee;
using BXH.AutomatedTests.Configs;
using NUnit.Framework;

namespace BXH.AutomatedTests.Test.Tests.Apigee
{
    [TestFixture]
    public class ApigeeTests
    {
        private static readonly TestHelper TestConfigs = new TestHelper();
        private readonly ApigeeApiTests _apigeeTests = new ApigeeApiTests(TestConfigs);

        [Test]
        public void PostAdvancedShipReturnsOk()
        {
            var result = _apigeeTests.ShipNotices("HappyPath");

            Assert.That(result, Is.Not.Null);
            StringAssert.Contains("SUCCESS", result);
        }

        [Test]
        public void PostAdvancedShipReturnsInternalServerErrorWithInvalidXml()
        {
            var result = _apigeeTests.ShipNotices("InvalidXML");

            Assert.That(result, Is.Not.Null);
            StringAssert.Contains("SUCCESS", result);
        }

        [Test]
        public void PostReturnsUnauthorizedWithInvalidToken()
        {
            _apigeeTests.useInvalidToken = true;
            var result = _apigeeTests.ShipNotices("InvalidToken");
            _apigeeTests.useInvalidToken = false;

            Assert.That(result, Is.Not.Null);
            StringAssert.Contains("SUCCESS", result);
        }

        [Test]
        public void PostReturnsUnauthorizedWithInvalidApiKey()
        {
            _apigeeTests.useInvalidApiKey = true;
            var result = _apigeeTests.ShipNotices("InvalidApiKey");
            _apigeeTests.useInvalidApiKey = false;

            Assert.That(result, Is.Not.Null);
            StringAssert.Contains("SUCCESS", result);
        }

        [Test]
        public void PostBulkShipStatusReturnsOk()
        {
            var result = _apigeeTests.BulkShipStatus("HappyPath");

            Assert.That(result, Is.Not.Null);
            StringAssert.Contains("SUCCESS", result);
        }

        [Test]
        public void PostBulkShipStatusReturnsInternalServerErrorWithInvalidXml()
        {
            var result = _apigeeTests.BulkShipStatus("InvalidXML");

            Assert.That(result, Is.Not.Null);
            StringAssert.Contains("SUCCESS", result);
        }

        [Test]
        public void PostBlendingsReturnsOk()
        {
            var result = _apigeeTests.PostBlendings("HappyPath");

            Assert.That(result, Is.Not.Null);
            StringAssert.Contains("SUCCESS", result);
        }
    }
}
