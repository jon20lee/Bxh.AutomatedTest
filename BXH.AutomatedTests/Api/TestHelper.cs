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
        public EnvironmentConfig configs;
        public List<TestTarget> TestTargets;
        public Logger Logger;
        public RestClient Client;
        public List<TestResult> TestResults;
        public string AppName;

        public TestHelper(string appName)
        {
            // Create Logger
            Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            Logger.Information($"--------------------- {appName} ------------------------");
            TestResults = new List<TestResult>();
            AppName = appName;

            //read in config settings
            string configFile = File.ReadAllText(Path.Combine("C:\\BXH", "AutomatedTestConfig.json"));
            dynamic envToTest = JObject.Parse(configFile).SelectToken($"EnvironmentToTest");

            //grab the configs and targets to be run
            configs = JObject.Parse(configFile).SelectToken($"Apps.{appName}.Environments[0].{envToTest.Value}").ToObject<EnvironmentConfig>();
            TestTargets = JObject.Parse(configFile).SelectToken($"Apps.{appName}.Targets").ToObject<List<TestTarget>>();

            Client = new RestClient(configs.HostURL);
        }

        public TestResult RunTest(TestTarget test, string token, string clientID)
        {

            TestResult testResult = new TestResult();

            var request = GetTargetRestRequest(test, token, clientID);
            var res = Client.Execute(request);
            testResult.Response = res;
            testResult.TestName = test.Name;

            Logger.Information($"------------------------------------------------------");
            Logger.Information($"Test Name: {testResult.TestName} ");
            Logger.Information($"Test status code: {testResult.Response.StatusCode} ");
            var finalTestResult = testResult.Response.StatusCode == HttpStatusCode.OK ? "PASSED" : "FAILED";
            Logger.Information($"Test Result: {finalTestResult}");
        
            return testResult;
        }

        public RestRequest GetTargetRestRequest(TestTarget test, string token, string clientId)
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
                    request.AddHeader(header.key, header.value);
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
    }
}