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
        public TestHelper testHelper;
        public Logger _logger;
        public TestApplication testApplication;
        public bool useInvalidApiKey;
        public bool useInvalidToken = false;

        public BxhApiTests(TestHelper conf)
        {
            testHelper = conf;
            _logger = conf.Logger;
            testApplication = testHelper.GetTestApplication("BXH");
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

        public string GetBlendingV2(string testCase)
        {
            return ExecuteBxhTest("BlendingsV2", testCase);
        }

        public string GetInventory(string testCase)
        {
            return ExecuteBxhTest("Inventory", testCase);
        }

        public string PostDeliveryConfirmations(string testCase)
        {
            return ExecuteBxhTest("DeliveryConfirmations", testCase);
        }

        public string ExecuteBxhTest(string testName, string testCase)
        {

            TestTarget testTarget = (TestTarget)testApplication.Targets.FirstOrDefault(x => x.Name == testName);
            TestTargetApiKey envApiKey = (TestTargetApiKey)testApplication.Environments[0].ApiKey;
            TestTargetTestCases tc = testTarget?.TestCases.FirstOrDefault(x => x.name == testCase);

            var res = testHelper.RunTest(testTarget, testApplication.Client, testCase, useInvalidApiKey ? "" : envApiKey.value, "");

            if (res?.Response.StatusCode == (HttpStatusCode)Enum.Parse(typeof(HttpStatusCode), tc.resultCode.ToString()))
            {
                return $"SUCCESS: Status: {res?.Response.StatusCode}";
            }

            return $"FAILURE: {res?.Response.StatusCode} : {res?.Response.ErrorMessage}";
        }
    }
}