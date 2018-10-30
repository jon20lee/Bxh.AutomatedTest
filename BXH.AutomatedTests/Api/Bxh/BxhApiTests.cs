using System.Linq;
using System.Net;
using BXH.AutomatedTests.Api.Apigee;
using BXH.AutomatedTests.Api.Models;
using RestSharp;
using Serilog.Core;

namespace BXH.AutomatedTests.Api.Bxh
{
    public class BxhApiTests
    {
        private TestHelper testHelper;
        private Logger _logger;

        public BxhApiTests(TestHelper conf)
        {
            testHelper = conf;
            _logger = conf.Logger;
        }

        public string ShipNotices()
        {
            return ExecuteBxhTest("AdvancedShipNotice");
        }

        public string BulkShipStatus()
        {
            return ExecuteBxhTest("BulkShipStatus");
        }

        public string ExecuteBxhTest(string testName)
        {

            TestTarget testsToRun = (TestTarget)testHelper.TestTargets.FirstOrDefault(x => x.Name == testName);

            var res = testHelper.RunTest(testsToRun,"", "");

            if (res?.Response.StatusCode == HttpStatusCode.OK)
            {
                return $"SUCCESS: Status: {res?.Response.StatusCode}";
            }

            return $"FAILURE: {res?.Response.StatusCode} : {res?.Response.ErrorMessage}";
        }
    }
}