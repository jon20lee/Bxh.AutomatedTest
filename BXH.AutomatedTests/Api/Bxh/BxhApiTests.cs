using System;
using System.Collections.Generic;
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

        public string ShipNotices(string testCase)
        {
            return ExecuteBxhTest("AdvancedShipNotice", testCase);
        }

        public string BulkShipStatus(string testCase)
        {
            return ExecuteBxhTest("BulkShipStatus", testCase);
        }

        public string PostBlending(string testCase)
        {
            return ExecuteBxhTest("Blendings", testCase);
        }

        public string ExecuteBxhTest(string testName, string testCase)
        {

            TestTarget testTarget = (TestTarget) testHelper.TestTargets.FirstOrDefault(x => x.Name == testName);
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