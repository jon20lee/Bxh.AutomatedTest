

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml;
using BXH.AutomatedTests.Api.Apigee.Data;
using BXH.AutomatedTests.Api.Apigee.Models;
using BXH.AutomatedTests.Configs;
using Newtonsoft.Json.Linq;
using RestSharp;
using SimpleJson;

namespace BXH.AutomatedTests.Api.Apigee
{
    public class ApigeeProxyTests
    {
        public string token;
        public ApigeeConfig apigeeConfigs;
        public List<TestTarget> testTargets;
        public RestClient client;
        public List<IRestResponse> responses;

        public ApigeeProxyTests()
        {
            //read in config settings
            string configFile = File.ReadAllText(Path.Combine("C:\\BXH", "AutomatedTestConfig.json"));
            dynamic envToTest = JObject.Parse(configFile).SelectToken($"EnvironmentToTest");
            responses = new List<IRestResponse>();

            //grab the configs and targets to be run
            apigeeConfigs = JObject.Parse(configFile).SelectToken($"Apps.Apigee.Environments[0].{envToTest.Value}").ToObject<ApigeeConfig>();
            testTargets = JObject.Parse(configFile).SelectToken($"Apps.Apigee.Targets").ToObject<List<TestTarget>>();

            client = new RestClient(apigeeConfigs.HostURL);
        }

        public List<IRestResponse> RunTests(string testName)
        {
            IEnumerable<TestTarget> testsToRun = testTargets.Any(x => x.Name == testName) ? (IEnumerable<TestTarget>)testTargets.Where(x => x.Name == testName) : testTargets;

            foreach (var test in testsToRun)
            {
                var request = GetTargetRestRequest(test);
                var res = client.Execute(request);
                responses.Add((res));
            }

            return responses;
        }

        public RestRequest GetTargetRestRequest(TestTarget test)
        {
            //get token for test...
            ApigeeProductApp tokenConfigs = apigeeConfigs.ProductApps.FirstOrDefault(x => x.ID == test.ProductAppID);
            var token = ApigeeToken(tokenConfigs.ClientID, tokenConfigs.ClientSecret);

            //setup request
            var request = new RestRequest(test.TargetURL, (Method)Enum.Parse(typeof(Method), test.HTTPVerb));

            //add headers
            foreach (var header in test.Headers)
            {
                if (header.key == "api-key")
                {
                    request.AddHeader(header.key, tokenConfigs.ClientID);
                }
                else if (header.key == "Authorization")
                {
                    request.AddHeader(header.key, header.value + " " + token);
                }
                else
                {
                    request.AddHeader(header.key, header.value);
                }
            }

            //add params
            foreach (var param in test.Parameters)
            {
                request.AddParameter(param.key, param.value, (ParameterType)Enum.Parse(typeof(ParameterType), param.type));
            }

            return request;
        }

        public string ApigeeToken(string clientId, string clientSecret)
        {

            var request = new RestRequest(Dev.APIGEE_RESOURCE_TOKEN, Method.POST);
            request.AddQueryParameter("grant_type", "client_credentials");
            request.AddParameter(
                "application/x-www-form-urlencoded",
                $"client_id={clientId}&client_secret={clientSecret}",
                ParameterType.RequestBody);

            var tokenResponse = client.Execute<ApigeeTokenResponse>(request);

            if (tokenResponse.StatusCode == HttpStatusCode.OK)
            {
                return tokenResponse.Data.access_token;
            }

            return "";
        }

        public string ShipNotices()
        {

            var res =  RunTests("AdvancedShipNotice");

            if (res?.FirstOrDefault()?.StatusCode == HttpStatusCode.OK)
            {
                return $"SHIP NOTICE SUCCESS: Status: {res?.FirstOrDefault()?.StatusCode}";
            }

            return $"SHIP NOTICE endpoint failed: {res?.FirstOrDefault()?.StatusCode} : {res?.FirstOrDefault()?.ErrorMessage}";
        }

        public string BulkShipStatus()
        {
            var res = RunTests("BulkShipStatus");

            if (res?.FirstOrDefault()?.StatusCode == HttpStatusCode.OK)
            {
                return $"BULK SHIP NOTICE SUCCESS: Status: {res?.FirstOrDefault()?.StatusCode}";
            }

            return $"BULK SHIP NOTICE endpoint failed: {res?.FirstOrDefault()?.StatusCode} : {res?.FirstOrDefault()?.ErrorMessage}";
        }
    }
}