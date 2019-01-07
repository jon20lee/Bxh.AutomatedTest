using System;
using System.Threading;
using System.Net.Mime;
using BXH.AutomatedTests.Api;
using BXH.AutomatedTests.Configs;
using BXH.AutomatedTests.Api.Apigee;

using NUnit.Framework;


namespace BXH.AutomatedTests.Test.Tests.Apigee
{
    [TestFixture]
    public class ApigeeTests
    {
        private static readonly TestHelper TestConfigs = new TestHelper();
        private readonly ApigeeApiTests _apigeeTests = new ApigeeApiTests(TestConfigs);

        //SOM - Advance Ship Notice
        [Test]
        public void PostAdvancedShipReturnsOk()
        {
            int? _itr = _apigeeTests.testApplication.Targets[0].TestCases[0].iterations;
            if (_itr <= 0)
            {
                _itr = 1;
            }
            else
            {
                for (int i = 0; i < _itr; i++)
                {
                    var result = _apigeeTests.ShipNotices("HappyPath");

                    Assert.That(result, Is.Not.Null);
                    StringAssert.Contains("SUCCESS", result);
                }
            }
        }

        [Test]
        // Currently passes with a chemical payload, but need a PL with seed
        // Passes: Test Case 9, using payload 1 (chemical), type parameter 7
        // Fails:  Test Case 9, using payload 10 (seed), type parameter 7
        // Need Seed payload
        public void PostAdvancedShipSeedReturnsOk()
        {
            Thread.Sleep(250);
            var result = _apigeeTests.ShipNotices("ValidateSeedParameter");

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
        public void PostAdvancedShipReturnsInternalServerErrorWithXMLThreat()
        {
            var result = _apigeeTests.ShipNotices("XMLThreat");

            Assert.That(result, Is.Not.Null);
            StringAssert.Contains("SUCCESS", result);
        }

        [Test]
        public void PostAdvancedShipOnRequriedParam()
        {   //Validates Required Parameter is missinging
            var result = _apigeeTests.ShipNotices("ValidateTypeParameter");

            Assert.That(result, Is.Not.Null);
            StringAssert.Contains("SUCCESS", result);
        }

        [Test]
        public void PostAdvancedValidateMissTypedTypeParam()
        {   //Validates Required Parameter is missinging
            var result = _apigeeTests.ShipNotices("MissTypedTypeParameter");

            Assert.That(result, Is.Not.Null);
            StringAssert.Contains("SUCCESS", result);
        }

        [Test]
        public void PostAdvancedValidateUnexpectedTypeParam()
        {   //Validates Required Parameter is missinging
            var result = _apigeeTests.ShipNotices("UNETypeParameter");

            Assert.That(result, Is.Not.Null);
            StringAssert.Contains("SUCCESS", result);
        }

        [Test]
        public void PostAdvancedInValidGLN()
        {   //Validates Required Parameter is missinging
            var result = _apigeeTests.ShipNotices("InvalidGLN");

            Assert.That(result, Is.Not.Null);
            StringAssert.Contains("SUCCESS", result);
        }

        [Test]
        public void PostAdvancedValidateGLNTag()
        {   //Validates Required Parameter is missinging
            var result = _apigeeTests.ShipNotices("InvalidateGLNTag");

            Assert.That(result, Is.Not.Null);
            StringAssert.Contains("SUCCESS", result);
        }

        // Token testing
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

        // SOM - Bulk Ship Notice
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
        public void PostBulkShipStatusReturnsInternalServerErrorWithXMLThreat()
        {
            var result = _apigeeTests.BulkShipStatus("XMLThreat");

            Assert.That(result, Is.Not.Null);
            StringAssert.Contains("SUCCESS", result);
        }

        [Test]
        public void PostBulkShipStatusSendstoAddress()
        {
            var result = _apigeeTests.BulkShipStatus("ToAddressEmailing");

            Assert.That(result, Is.Not.Null);
            StringAssert.Contains("SUCCESS", result);
        }

        // SOM - Inventory (IAU)
        [Test]
        public void GetInventory()
        {
            int? _itr = _apigeeTests.testApplication.Targets[3].TestCases[0].iterations;
            if (_itr <= 0)
            {
                _itr = 1;
            }
            else
            {
                for (int i = 0; i < _itr; i++)
                {
                    var result = _apigeeTests.GetInventory("HappyPath");

                    Assert.That(result, Is.Not.Null);
                    StringAssert.Contains("SUCCESS", result);

                    System.Threading.Thread.Sleep(5000);
                }
            }
        }

        //Date for inventory 
        [Test]
        public void GetInventoryWithDate()
        {
            var result = _apigeeTests.GetInventory("DateParameter");

            Assert.That(result, Is.Not.Null);
            StringAssert.Contains("SUCCESS", result);
            System.Threading.Thread.Sleep(5000);
        }

        [Test]
        public void GetInventoryInvalidMonthBegin()
        {
            var result = _apigeeTests.GetInventory("BeginMonth");

            Assert.That(result, Is.Not.Null);
            StringAssert.Contains("SUCCESS", result);
            System.Threading.Thread.Sleep(5000);
        }

        [Test]
        public void GetInventoryInvalidMonthEnd()
        {
            var result = _apigeeTests.GetInventory("EndMonth");

            Assert.That(result, Is.Not.Null);
            StringAssert.Contains("SUCCESS", result);
            System.Threading.Thread.Sleep(5000);
        }

        [Test]
        public void GetInventoryValidateURL()
        {
            var result = _apigeeTests.GetInventory("ValidateURL");
            Assert.That(result, Is.Not.Null);
            StringAssert.Contains("SUCCESS", result);

            //var resultDownload = getURL.FromInvenotryURL();
            //Assert.That(resultDownload, Is.Not.Null);
            //Assert.Contains("SUCCESS", resultDownload);

            System.Threading.Thread.Sleep(5000);
        }

        //Delievery Confirmations (currently stub, set status 500)
        [Test]
        public void GetDeliveryConfirmations()
        {
            var result = _apigeeTests.GetDeliveryConfirmations("HappyPath");

            Assert.That(result, Is.Not.Null);
            StringAssert.Contains("SUCCESS", result);
            System.Threading.Thread.Sleep(5000);
        }

        // Blending
        [Test]
        public void PostBlendingsReturnsOk()
        {
            var result = _apigeeTests.PostBlendings("HappyPath");

            Assert.That(result, Is.Not.Null);
            StringAssert.Contains("SUCCESS", result);
        }

        [Test]
        public void PostBlendingsInvaildJsonThreat()
        {
            var result = _apigeeTests.PostBlendings("InvalidJsonThreat");

            Assert.That(result, Is.Not.Null);
            StringAssert.Contains("SUCCESS", result);
        }

        [Test]
        public void PostBlendingsInvalidJsonHeader()
        {
            var result = _apigeeTests.PostBlendings("InvalidJsonHeader");

            Assert.That(result, Is.Not.Null);
            StringAssert.Contains("SUCCESS", result);
        }
    }
}