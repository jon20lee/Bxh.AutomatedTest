using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BXH.AutomatedTests.Api;
using BXH.AutomatedTests.Api.Apigee;
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
            TestHelper TestHelpers = new TestHelper("Apigee");
            ApigeeApiTests _apigeeTests = new ApigeeApiTests(TestHelpers);

            foreach (var test in TestHelpers.TestTargets)
            {
                var appconfig = TestHelpers.configs.ProductApps.FirstOrDefault(x => x.ID == test.ProductAppID);

                TestHelpers.RunTest(test, "",  _apigeeTests.ApigeeToken(appconfig.ClientID, appconfig.ClientSecret), appconfig.ClientID);
            }

            TestHelpers = new TestHelper("BXH");

            foreach (var test in TestHelpers.TestTargets)
            {
                TestHelpers.RunTest(test, "", "", "");
            }

            TestHelpers = new TestHelper("CORE");

            foreach (var test in TestHelpers.TestTargets)
            {
                TestHelpers.RunTest(test, "", "", "");
            }

            
            TestHelpers = new TestHelper("INNER");
            InnerApiTests _innerTests = new InnerApiTests(TestHelpers);

            foreach (var test in TestHelpers.TestTargets)
            {
                TestTargetCredentials innerCreds = (TestTargetCredentials)TestHelpers.configs.credentials;

                TestHelpers.RunTest(test, "", _innerTests.InnerToken(innerCreds.username, innerCreds.password), "");
            }

            Console.ReadLine();
        }
    }
}
