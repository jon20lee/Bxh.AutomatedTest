using System.Linq;
using System.Net;
using BXH.AutomatedTests.Api.Models;
using Serilog.Core;

namespace BXH.AutomatedTests.Api
{
    public class CoreApiTests
    {
        private TestHelper testHelper;
        private Logger _logger;

        public CoreApiTests(TestHelper conf)
        {
            testHelper = conf;
            _logger = conf.Logger;
        }

        public string ShipNotices()
        {
            return ExecuteCoreTest("AdvancedShipNotice");
        }

        public string BulkShipStatus()
        {
            return ExecuteCoreTest("BulkShipStatus");
        }

        public string PostBlending()
        {
            return ExecuteCoreTest("Blendings");
        }

        public string ExecuteCoreTest(string testName)
        {

            TestTarget testsToRun = (TestTarget)testHelper.TestTargets.FirstOrDefault(x => x.Name == testName);

            var res = testHelper.RunTest(testsToRun, "", "");

            if (res?.Response.StatusCode == HttpStatusCode.OK)
            {
                return $"SUCCESS: Status: {res?.Response.StatusCode}";
            }

            return $"FAILURE: {res?.Response.StatusCode} : {res?.Response.ErrorMessage}";
        }
    }
}