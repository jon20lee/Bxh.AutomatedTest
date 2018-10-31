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
        private TestHelper testHelper;
        private Logger _logger;

        public InnerApiTests(TestHelper conf)
        {
            testHelper = conf;
            _logger = conf.Logger;
        }

        public string ShipNotices()
        {
            return ExecuteInnerTest("AdvancedShipNotice");
        }

        public string BulkShipStatus()
        {
            return ExecuteInnerTest("BulkShipStatus");
        }

        public string PostBlending()
        {
            return ExecuteInnerTest("Blendings");
        }


        public string InnerToken(string clientId, string clientSecret)
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
            var request = new RestRequest("/token", Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");

            request.AddParameter("username", clientId, ParameterType.GetOrPost);
            request.AddParameter("grant_type", "password", ParameterType.GetOrPost);
            request.AddParameter("password", clientSecret, ParameterType.GetOrPost);

            var tokenResponse = testHelper.Client.Execute<InnerTokenResponse>(request);

            if (tokenResponse.StatusCode == HttpStatusCode.OK)
            {
                return tokenResponse.Data.access_token;
            }

            return "";
        }

        public string ExecuteInnerTest(string testName)
        {

            TestTarget testsToRun = (TestTarget)testHelper.TestTargets.FirstOrDefault(x => x.Name == testName);
            TestTargetCredentials innerCreds = (TestTargetCredentials)testHelper.configs.credentials;


            var res = testHelper.RunTest(testsToRun, InnerToken(innerCreds.username, innerCreds.password), "");

            if (res?.Response.StatusCode == HttpStatusCode.OK)
            {
                return $"SUCCESS: Status: {res?.Response.StatusCode}";
            }

            return $"FAILURE: {res?.Response.StatusCode} : {res?.Response.ErrorMessage}";
        }
    }
}