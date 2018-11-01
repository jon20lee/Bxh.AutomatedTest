using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using BXH.AutomatedTests.Api.Models;
using Newtonsoft.Json.Linq;
using RestSharp;
using Serilog;
using Serilog.Core;

namespace BXH.AutomatedTests.Api
{
    public class TestHelper 
    {
        public List<TestApplication> testApps;
        public List<TestTarget> TestTargets;
        public List<TestResult> TestResults;
        public Logger Logger;
        public RestClient Client;
        public string AppName;
        public string EnvironemntToTest;

        public TestHelper()
        {
            // Create Logger
            Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            TestResults = new List<TestResult>();

            //read in config settings
            string configFile = File.ReadAllText(Path.Combine("C:\\BXH", "AutomatedTestConfig.json"));
            EnvironemntToTest = (string) JObject.Parse(configFile).SelectToken($"EnvironmentToTest");

            //grab the configs and targets to be run
            testApps = JObject.Parse(configFile).SelectToken("Apps").ToObject<List<TestApplication>>();
        }

        public TestApplication GetTestApplication(string appName)
        {
            var app = new TestApplication();

            Logger.Information($"~~~~~~~~~~~~~~~~~~~~  {appName}  ~~~~~~~~~~~~~~~~~~~~~~~~~");

            app.Targets = testApps.FirstOrDefault(x => x.Name == appName)?.Targets;
            app.Environments = new List<EnvironmentConfig>(testApps?.FirstOrDefault(x => x.Name == appName)?.Environments?.Where(x => x.Name == EnvironemntToTest));
            app.Client = new RestClient(app.Environments[0].HostURL);
            return app;
        }

        public RestRequest GetTargetRestRequest(TestTarget test, TestTargetTestCases testCase, string token, string clientId)
        {

            //setup request
            var request = new RestRequest(test.TargetURL, (Method)Enum.Parse(typeof(Method), test.HTTPVerb));

            //add headers
            foreach (var header in test.Headers)
            {
                if (header.key == "api-key")   //only used for apigee for now...
                {
                    request.AddHeader(header.key, clientId);
                }
                else if (header.key == "Authorization" && header.value == "Bearer")
                {
                    request.AddHeader(header.key, header.value + " " + token);
                }
                else if (header.key == "x-api-key")
                {
                    request.AddHeader(header.key, token);
                }
                else
                {
                    request.AddHeader(header.key, header.value);
                }
            }

            IEnumerable<TestTargetParameters> parameters = test.Parameters.Where(x => x.id == testCase.paramId);

            //add params
            foreach (var param in parameters)
            {
                request.AddParameter(param.key, param.value, (ParameterType)Enum.Parse(typeof(ParameterType), param.type));
            }

            return request;
        }

        public TestResult RunTest(TestTarget test, RestClient client, string testCase, string token, string clientID)
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
            TestTargetTestCases tc = test.TestCases.FirstOrDefault(x => x.name == testCase);
            TestResult testResult = new TestResult();

            var request = GetTargetRestRequest(test, tc, token, clientID);
            var res = client.Execute(request);
            testResult.Response = res;
            testResult.TestName = test.Name;
            testResult.Status = testResult.Response.StatusCode == (HttpStatusCode)Enum.Parse(typeof(HttpStatusCode), tc.resultCode.ToString()) ? "PASSED" : "FAILED";

            TestResults.Add(testResult);

            Logger.Information($"------------------------------------------------------");
            Logger.Information($"Test Name: {testResult.TestName} ");
            Logger.Information($"Test Case: {tc.name} ");
            Logger.Information($"Test status code: {testResult.Response.StatusCode} ");
            if (testResult.Status == "PASSED")
            {
                Logger.Information($"Test Result: {testResult.Status}");
            }
            else
            {
                Logger.Error($"Test Result: {testResult.Status}");
            }
            
        
            return testResult;
        }

    }
}