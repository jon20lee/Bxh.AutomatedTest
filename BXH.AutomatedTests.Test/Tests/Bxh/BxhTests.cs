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
            int? _itr = _bxhTests.testApplication.Targets[0].TestCases[0].iterations;
            if (_itr <= 0)
            {
                _itr = 1;
            }
            else
            {
                for (int i = 0; i < _itr; i++)
                {
                    var result = _bxhTests.ShipNotices("HappyPath");

                    Assert.That(result, Is.Not.Null);
                    StringAssert.Contains("SUCCESS", result);

                    System.Threading.Thread.Sleep(5000);
                }
            }
        }

        [Test]
        public void PostAdvancedShipReturns500InvalidVendor()
        {
            var result = _bxhTests.ShipNotices("InvalidVendorNumber");

            Assert.That(result, Is.Not.Null);
            StringAssert.Contains("SUCCESS", result);
        }

        [Test]
        public void PostAdvancedShipReturnsInvalidXml()
        {
            var result = _bxhTests.ShipNotices("InvalidXML");

            Assert.That(result, Is.Not.Null);
            StringAssert.Contains("SUCCESS", result);
        }

        //VerifySeedPayload
        [Test]
        public void PostAdvancedShipSeedPayloadOk()
        {
            var result = _bxhTests.ShipNotices("VerifySeedPayload");

            Assert.That(result, Is.Not.Null);
            StringAssert.Contains("SUCCESS", result);
        }

        [Test]
        // Currently passes with a chemical payload, but need a PL with seed
        // Passes: Test Case 6, using payload 1 (chemical), type parameter 7
        // Fails:  Test Case 6, using payload 4 (seed), type parameter 7
        // Need Seed payload
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

        [Test]
        public void GetBlendingV2ReturnsOk()
        {
            var result = _bxhTests.GetBlendingV2("HappyPath");

            Assert.That(result, Is.Not.Null);
            StringAssert.Contains("SUCCESS", result);
        }

/*
        [Test]
        public void PostBlendingV2ReturnsError()
        {
            var result = _bxhTests.GetBlendingV2("InvalidHeader");

            Assert.That(result, Is.Not.Null);
            StringAssert.Contains("SUCCESS", result);
        }
*/
        [Test]
        public void PostBulkShipStatusSendstoAddress()
        {
            var result = _bxhTests.BulkShipStatus("ToAddressEmailing");

            Assert.That(result, Is.Not.Null);
            StringAssert.Contains("SUCCESS", result);
        }

        [Test]
        //Currently NoOp, lower level is not validating JSON
        public void PostBlendingReturnsInvalidJson()
        {
            var result = _bxhTests.PostBlending("InvalidJson");
         
            Assert.That(result, Is.Not.Null);
            StringAssert.Contains("SUCCESS", result);
        }

        [Test]
        public void GetInventory()
        {
            var result = _bxhTests.GetInventory("HappyPath");

            Assert.That(result, Is.Not.Null);
            StringAssert.Contains("SUCCESS", result);
        }

        //Date for inventory 
        [Test]
        public void GetInventoryWithDate()
        {
            var result = _bxhTests.GetInventory("DateParameter");

            Assert.That(result, Is.Not.Null);
            StringAssert.Contains("SUCCESS", result);
            System.Threading.Thread.Sleep(5000);
        }

        //Get Delivery Confirmations
        [Test]
        public void PostDelieveryConfirmations()
        {
            var result = _bxhTests.PostDeliveryConfirmations("HappyPath");

            Assert.That(result, Is.Not.Null);
            StringAssert.Contains("SUCCESS", result);
        }

        [Test]
        public void PostDeliveryConfirmationsInvalidGLN()
        {
            var result = _bxhTests.PostDeliveryConfirmations("InvalidGLN");

            Assert.That(result, Is.Not.Null);
            StringAssert.Contains("SUCCESS", result);
        }

        [Test]
        public void PostDeliveryConfirmationsInvalidXML()
        {
            var result = _bxhTests.PostDeliveryConfirmations("InvalidXML");

            Assert.That(result, Is.Not.Null);
            StringAssert.Contains("SUCCESS", result);
        }
    }
}