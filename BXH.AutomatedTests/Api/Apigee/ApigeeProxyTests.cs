using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using BXH.AutomatedTests.Api.Models;
using RestSharp;
using Serilog.Core;

namespace BXH.AutomatedTests.Api.Apigee
{
    public class ApigeeProxyTests
    {

        private TestHelper testHelper;
        private Logger _logger;

        public ApigeeProxyTests(TestHelper conf)
        {
            testHelper = conf;
            _logger = conf.Logger;
        }

        public string ApigeeToken(string clientId, string clientSecret)
        {
            var request = new RestRequest("/oauth/client_credential/accesstoken", Method.POST);
            request.AddQueryParameter("grant_type", "client_credentials");
            request.AddParameter(
                "application/x-www-form-urlencoded",
                $"client_id={clientId}&client_secret={clientSecret}",
                ParameterType.RequestBody);

            var tokenResponse = testHelper.Client.Execute<ApigeeTokenResponse>(request);

            if (tokenResponse.StatusCode == HttpStatusCode.OK)
            {
                return tokenResponse.Data.access_token;
            }

            return "";
        }

        public string ShipNotices()
        {
            return ExecuteApigeeTest("AdvancedShipNotice");
        }

        public string BulkShipStatus()
        {
            return ExecuteApigeeTest("BulkShipStatus");
        }

        public string ExecuteApigeeTest(string testName)
        {

            TestTarget testsToRun = (TestTarget)testHelper.TestTargets.FirstOrDefault(x => x.Name == testName);
            ProductApp apigeeApp = (ProductApp)testHelper.configs.ProductApps.FirstOrDefault(x => x.ID == testsToRun.ProductAppID);

            var res = testHelper.RunTest(testsToRun, ApigeeToken(apigeeApp.ClientID, apigeeApp.ClientSecret), apigeeApp.ClientID);

            if (res?.Response.StatusCode == HttpStatusCode.OK)
            {
                return $"SUCCESS: Status: {res?.Response.StatusCode}";
            }

            return $"FAILURE: {res?.Response.StatusCode} : {res?.Response.ErrorMessage}";
        }
    }
}