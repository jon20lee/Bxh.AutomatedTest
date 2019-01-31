using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BXH.AutomatedTests.Api;
using BXH.AutomatedTests.Api.Apigee;
using BXH.AutomatedTests.Api.Bxh;
using BXH.AutomatedTests.Api.Inner;
using BXH.AutomatedTests.Api.Models;
using BXH.AutomatedTests.Configs;
using Serilog;
using Serilog.Core;

namespace BXH.AutomatedTests
{
    class Program
    {

        static void Main(string[] args)
        {
            TestHelper TestHelpers = new TestHelper();

            Stopwatch sw = new Stopwatch();
            sw.Start();
            ApigeeApiTests _apigeeTests = new ApigeeApiTests(TestHelpers);
            var testapp = _apigeeTests.testApplication;
            
            foreach (var test in testapp.Targets)
            {
                ProductApp apigeeApp = (ProductApp)testapp.Environments[0].ProductApps.FirstOrDefault(x => x.ID == test?.ProductAppID);
                foreach (var testCase in test.TestCases)
                {
                    if (testCase.name == "InvalidToken")
                    {
                        _apigeeTests.useInvalidToken = true;
                    }

                    if (testCase.name == "InvalidApiKey")
                    {
                        _apigeeTests.useInvalidApiKey = true;
                    }

                    //if (testCase.name == "HappyPath")
                    //{
                    //  Console.WriteLine("Test: Running " + testCase.name);

                        TestHelpers.RunTest(test, testapp.Client, testCase.name, _apigeeTests.useInvalidToken ? "" : _apigeeTests.ApigeeToken(apigeeApp?.ClientID, apigeeApp?.ClientSecret), _apigeeTests.useInvalidApiKey ? "" : apigeeApp?.ClientID);
                    
                    //}
                    _apigeeTests.useInvalidToken = false;
                    _apigeeTests.useInvalidApiKey = false;
                }
            }
            //*
            BxhApiTests _bxhTests = new BxhApiTests(TestHelpers);
            var bxhTestApp = _bxhTests.testApplication;

            foreach (var test in bxhTestApp.Targets)
            {
                foreach (var testCase in test.TestCases)
                {
                    if (testCase.name == "InvalidApiKey")
                    {
                        _bxhTests.useInvalidApiKey = true;
                    }

                    TestHelpers.RunTest(test, bxhTestApp.Client, testCase.name, _bxhTests.useInvalidApiKey ? "" : test.Headers.FirstOrDefault(x => x.key == "x-api-key").value, "");

                    _bxhTests.useInvalidApiKey = false;
                }
            }
            //*/
            CoreApiTests _coreTests = new CoreApiTests(TestHelpers);
            var coreTestApp = _coreTests._testApplication;

            foreach (var test in coreTestApp.Targets)
            {
                foreach (var testCase in test.TestCases)
                {
                    if (testCase.name == "InvalidApiKey")
                    {
                        _coreTests.useInvalidApiKey = true;
                    }

                    TestHelpers.RunTest(test, coreTestApp.Client, testCase.name, _coreTests.useInvalidApiKey ? "" : test.Headers.FirstOrDefault(x => x.key == "x-api-key").value, "");

                    _coreTests.useInvalidApiKey = false;
                }
            }

            InnerApiTests _innerTests = new InnerApiTests(TestHelpers);
            var innerTestApp = _innerTests._testApplication;

            foreach (var test in innerTestApp.Targets)
            {
                TestTargetCredentials innerCreds = (TestTargetCredentials)innerTestApp.Environments[0].credentials;

                foreach (var testCase in test.TestCases)
                {
                    TestHelpers.RunTest(test, innerTestApp.Client, testCase.name, _innerTests.InnerToken(innerCreds.username, innerCreds.password), "");

                    _apigeeTests.useInvalidToken = false;
                    _apigeeTests.useInvalidApiKey = false;
                }
            }
            sw.Stop();
            var passing = TestHelpers.TestResults.Count(x => x.Status == "PASSED");
            var failing = TestHelpers.TestResults.Count(x => x.Status == "FAILED");

            Console.WriteLine();
            Console.WriteLine();
            TestHelpers.Logger.Information("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine();
            TestHelpers.Logger.Information($"Elapsed time: {sw.Elapsed}");
            Console.WriteLine();
            TestHelpers.Logger.Information($"Tests Passing: {passing}");
            TestHelpers.Logger.Information($"Tests Failing: {failing}");

            Console.ReadLine();
        }
    }
}
