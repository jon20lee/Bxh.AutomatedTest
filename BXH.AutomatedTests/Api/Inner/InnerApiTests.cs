using System;
using System.Linq;
using System.Net;
using BXH.AutomatedTests.Api.Apigee;
using BXH.AutomatedTests.Api.Inner.Models;
using BXH.AutomatedTests.Api.Models;
using RestSharp;
using Serilog.Core;

namespace BXH.AutomatedTests.Api.Inner
{
    public class InnerApiTests
    {
        private readonly TestHelper _testHelper;
        private Logger _logger;
        public readonly TestApplication _testApplication;

        public InnerApiTests(TestHelper conf)
        {
            _testHelper = conf;
            _logger = conf.Logger;
            _testApplication = _testHelper.GetTestApplication("INNER");
        }

        public string ShipNotices(string testCase)
        {
            return ExecuteInnerTest("AdvancedShipNotice", testCase);
        }

        public string BulkShipStatus(string testCase)
        {
            return ExecuteInnerTest("BulkShipStatus", testCase);
        }

        public string PostBlending(string testCase)
        {
            return ExecuteInnerTest("Blendings", testCase);
        }

        public string GetInventory(string testCase)
        {
            return ExecuteInnerTest("Inventory", testCase);
        }

        public string InnerToken(string clientId, string clientSecret)
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
            var request = new RestRequest("/token", Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");

            request.AddParameter("username", clientId, ParameterType.GetOrPost);
            request.AddParameter("grant_type", "password", ParameterType.GetOrPost);
            request.AddParameter("password", clientSecret, ParameterType.GetOrPost);

            var tokenResponse = _testApplication.Client.Execute<InnerTokenResponse>(request);

            if (tokenResponse.StatusCode == HttpStatusCode.OK)
            {
                return tokenResponse.Data.access_token;
            }

            return "";
        }

        public string ExecuteInnerTest(string testName, string testCase)
        {

            TestTarget testTarget = (TestTarget)_testApplication.Targets.FirstOrDefault(x => x.Name == testName);
            TestTargetCredentials innerCreds = (TestTargetCredentials)_testApplication.Environments[0].credentials;
            TestTargetTestCases tc = testTarget?.TestCases.FirstOrDefault(x => x.name == testCase);

            var res = _testHelper.RunTest(testTarget, _testApplication.Client, testCase, InnerToken(innerCreds.username, innerCreds.password), "");

            if (res?.Response.StatusCode == (HttpStatusCode)Enum.Parse(typeof(HttpStatusCode), tc.resultCode.ToString()))
            {
                return $"SUCCESS: Status: {res?.Response.StatusCode}";
            }

            return $"FAILURE: {res?.Response.StatusCode} : {res?.Response.ErrorMessage}";
        }
    }
}