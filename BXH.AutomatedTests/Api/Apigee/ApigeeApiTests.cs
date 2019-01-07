using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using BXH.AutomatedTests.Api.Models;
using RestSharp;
using Serilog.Core;

namespace BXH.AutomatedTests.Api.Apigee
{
    public class ApigeeApiTests
    {

        public TestHelper testHelper;
        public TestApplication testApplication;
        private Logger _logger;
        public bool useInvalidToken = false;
        public bool useInvalidApiKey = false;
        public string currentFileURL;

        public ApigeeApiTests(TestHelper conf)
        {
            testHelper = conf;
            _logger = conf.Logger;
            testApplication = testHelper.GetTestApplication("Apigee");
        }

        public string ApigeeToken(string clientId, string clientSecret)
        {
            var request = new RestRequest("/token", Method.POST);
            request.AddQueryParameter("grant_type", "client_credentials");
            request.AddParameter(
                "application/x-www-form-urlencoded",
                $"client_id={clientId}&client_secret={clientSecret}",
                ParameterType.RequestBody);

            var tokenResponse = testApplication.Client.Execute<ApigeeTokenResponse>(request);

            if (tokenResponse.StatusCode == HttpStatusCode.OK)
            {
                return tokenResponse.Data.access_token;
            }

            return "";
        }

        public string ShipNotices(string testCase)
        {
            return ExecuteApigeeTest("AdvancedShipNotice", testCase);
        }

        public string BulkShipStatus(string testCase)
        {
            return ExecuteApigeeTest("BulkShipStatus", testCase);
        }

        public string PostBlendings(string testCase)
        {
            return ExecuteApigeeTest("Blendings", testCase);
        }

        public string GetInventory(string testCase)
        {
            return ExecuteApigeeTest("Inventory", testCase);
        }

        public string GetDeliveryConfirmations(string testCase)
        {
            return ExecuteApigeeTest("DeliveryConfirmations", testCase);
        }

        public string ExecuteApigeeTest(string testName, string testCase)
        {
            TestTarget testTarget = (TestTarget)testApplication.Targets.FirstOrDefault(x => x.Name == testName);
            ProductApp apigeeApp = (ProductApp)testApplication.Environments[0].ProductApps.FirstOrDefault(x => x.ID == testTarget?.ProductAppID);
            TestTargetTestCases tc = testTarget?.TestCases.FirstOrDefault(x => x.name == testCase);

                var res = testHelper.RunTest(testTarget, testApplication.Client, testCase, useInvalidToken ? "" : ApigeeToken(apigeeApp?.ClientID, apigeeApp?.ClientSecret), useInvalidApiKey ? "" : apigeeApp?.ClientID);

                if (res?.Response.StatusCode == (HttpStatusCode)Enum.Parse(typeof(HttpStatusCode), tc.resultCode.ToString()))
                {
                    return $"SUCCESS: Status: {res?.Response.StatusCode}";
                }

            return $"FAILURE: {res?.Response.StatusCode} : {res?.Response.ErrorMessage}";           
        }
    }
}