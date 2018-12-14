using System;
using System.Linq;
using System.Net;
using BXH.AutomatedTests.Api.Models;
using Serilog.Core;

namespace BXH.AutomatedTests.Api
{
    public class CoreApiTests
    {
        public readonly TestHelper _testHelper;
        public Logger _logger;
        public readonly TestApplication _testApplication;
        public bool useInvalidApiKey;

        public CoreApiTests(TestHelper conf)
        {
            _testHelper = conf;
            _logger = conf.Logger;
            _testApplication = _testHelper.GetTestApplication("CORE");
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

            TestTarget testTarget = (TestTarget)_testApplication.Targets.FirstOrDefault(x => x.Name == testName);
            TestTargetApiKey envApiKey = (TestTargetApiKey)_testApplication.Environments[0].ApiKey;
            TestTargetTestCases tc = testTarget?.TestCases.FirstOrDefault(x => x.name == testCase);

            var res = _testHelper.RunTest(testTarget, _testApplication.Client, testCase, useInvalidApiKey ? "" : envApiKey.value, "");

            if (res?.Response.StatusCode == (HttpStatusCode)Enum.Parse(typeof(HttpStatusCode), tc.resultCode.ToString()))
            {
                return $"SUCCESS: Status: {res?.Response.StatusCode}";
            }

            return $"FAILURE: {res?.Response.StatusCode} : {res?.Response.ErrorMessage}";
        }
    }
}