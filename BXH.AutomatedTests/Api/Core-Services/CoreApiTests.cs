using System;
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

        public string ShipNotices(string testCase)
        {
            return ExecuteCoreTest("AdvancedShipNotice", testCase);
        }

        public string BulkShipStatus(string testCase)
        {
            return ExecuteCoreTest("BulkShipStatus", testCase);
        }

        public string PostBlending(string testCase)
        {
            return ExecuteCoreTest("Blendings", testCase);
        }

        public string ExecuteCoreTest(string testName, string testCase)
        {

            TestTarget testTarget = (TestTarget)testHelper.TestTargets.FirstOrDefault(x => x.Name == testName);
            TestTargetTestCases tc = testTarget?.TestCases.FirstOrDefault(x => x.name == testCase);

            var res = testHelper.RunTest(testTarget, testCase, "", "");

            if (res?.Response.StatusCode == (HttpStatusCode)Enum.Parse(typeof(HttpStatusCode), tc.resultCode.ToString()))
            {
                return $"SUCCESS: Status: {res?.Response.StatusCode}";
            }

            return $"FAILURE: {res?.Response.StatusCode} : {res?.Response.ErrorMessage}";
        }
    }
}