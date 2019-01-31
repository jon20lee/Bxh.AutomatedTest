using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using BXH.AutomatedTests.Api.Models;
using Newtonsoft.Json.Linq;
using RestSharp;
using Serilog;
using Serilog.Core;
using System.Threading;

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
            List<TestTargetParameters> parameters = new List<TestTargetParameters>();
            testCase.paramId?.ForEach(x =>
            {
                var param = test.Parameters?.Where(y => y.id == x).FirstOrDefault();
                if(param != null)
                {
                    parameters.Add(test.Parameters?.Where(y => y.id == x).FirstOrDefault());
                }
            });

            //12-17-2018 converting T/t is a temp work-around
            if (EnvironemntToTest == "Dev" || EnvironemntToTest == "Test")
            {
                //add params
                foreach (var param in parameters)
                {
                    if (param.key.ToString() == "Type" && ( (param.value.ToString() == "chemical") || param.value.ToString() == "seed"))
                    {
                        String Key = param.key.ToLower();
                        String Value = param.value.ToLower();
                        request.AddParameter(Key, Value, (ParameterType)Enum.Parse(typeof(ParameterType), param.type));
                    }
                    else
                    {
                        request.AddParameter(param.key, param.value, (ParameterType)Enum.Parse(typeof(ParameterType), param.type));
                    }
                }
            }
            else if (EnvironemntToTest == "Stage" || EnvironemntToTest == "Sandbox")
            {
                foreach (var param in parameters)
                {
                        request.AddParameter(param.key, param.value, (ParameterType) Enum.Parse(typeof(ParameterType), param.type));
                }
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

            //* ----------------------------------------------------- *//
            var isError = res.Content.ToLower().Contains("iserror");
            testResult.Response = res;
            testResult.TestName = test.Name;

            if (!isError)
                testResult.Status = testResult.Response.StatusCode == (HttpStatusCode)Enum.Parse(typeof(HttpStatusCode), tc.resultCode.ToString()) ? "PASSED" : "FAILED";
            else
                testResult.Status = "PASSED";
            //* ------------------------------------------------------ *//
            
            TestResults.Add(testResult);

            Logger.Information($"------------------------------------------------------");
            Logger.Information($"Test Name: {testResult.TestName} ");
            Logger.Information($"Test Case: {tc.name} ");
            Logger.Information($"Test status code: {testResult.Response.StatusCode} ");
            if (testResult.Status == "PASSED")
            {
                Logger.Information($"Test Result: {testResult.Status}");
                if (testCase == "ValidateURL")
                {
                    var urlList = new List<string>();
                    var urlPassCount = 0;
                    var urlFailCount = 0;

                    var data = JObject.Parse(res.Content);
                    var urls = JArray.Parse(data["data"].ToString());
                    System.IO.File.WriteAllText(@"C:\BXH\InventoryURL.txt", res.Content);

                    for (int i = 0; i < urls.Count; i++)
                    {
                        var urlString = urls[i].ToString();
                        urlList.Add(urlString);

                        var request2 = new RestRequest(urlString, Method.GET);
                        var res2 = client.Execute(request2);

                        testResult.Response = res2;
                        testResult.TestName = test.Name;
                        testResult.Status = testResult.Response.StatusCode == (HttpStatusCode)Enum.Parse(typeof(HttpStatusCode), tc.resultCode.ToString()) ? "PASSED" : "FAILED";

                        TestResults.Add(testResult);

                        if (testResult.Status == "PASSED")
                        {
                            //return testResult;
                            urlPassCount++;
                        }
                        else
                        {
                            //Logger.Error($"Test Result: {testResult.Status}");
                            urlFailCount++;
                        }
                        if (urlPassCount == urls.Count)
                        {
                            return testResult;
                        }
                        else if (urlFailCount > 0)
                        {
                            Logger.Error($"Test Result: {testResult.Status}");
                        }
                    }
                }
                else if (testResult.TestName == "BlendingsV2")
                {
                    var data = JObject.Parse(res.Content);
                    var validMessage = (string)data["data"];   //Expected to contins the correct message;
                    if (!string.IsNullOrEmpty(validMessage))
                    {
                        var statusCode = (string)data["statusCode"];//.errorMessage;
                        if (validMessage.Contains("Invalid mixer id or branch Id provided") && (statusCode == "400"))
                        {
                            testResult.Status = "NotFound";
                        //tc.resultCode = 0;
                        }
                        else if (validMessage.Contains("No blend message found for mixerId") && (statusCode == "404"))
                        {
                            testResult.Status = "NotFound";
                            //tc.resultCode = 0;
                        }
                    }
                    else
                    {
                        validMessage = (string)data["errorMessage"];
                        testResult.Status = "FAILED";
                    }
                }
                else
                {
                    Logger.Error($"Test Result: {testResult.Status}");
                }
            }
            else
            {
                Logger.Error($"Test Result: {testResult.Status}");
            }
            return testResult;
        }
    }
}