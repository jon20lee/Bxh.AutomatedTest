using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BXH.AutomatedTests.Api;
using BXH.AutomatedTests.Api.Apigee;
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
            ApigeeProxyTests _apigeeTests = new ApigeeProxyTests(TestHelpers);

            foreach (var test in TestHelpers.TestTargets)
            {
                var appconfig = TestHelpers.configs.ProductApps.FirstOrDefault(x => x.ID == test.ProductAppID);

                TestHelpers.RunTest(test, _apigeeTests.ApigeeToken(appconfig.ClientID, appconfig.ClientSecret), appconfig.ClientID);
            }

            TestHelpers = new TestHelper("BXH");

            foreach (var test in TestHelpers.TestTargets)
            {
                TestHelpers.RunTest(test, "", "");
            }

            Console.ReadLine();
        }
    }
}
