using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BXH.AutomatedTests.Api.Apigee;
using BXH.AutomatedTests.Configs;

namespace BXH.AutomatedTests
{
    class Program
    {
        static void Main(string[] args)
        {

            ApigeeProxyTests apigeeTests = new ApigeeProxyTests();

            Console.WriteLine(" ---------------------  APIGEE TESTS  ----------------------------");
            Console.WriteLine("");
            Console.WriteLine(" ------------------------  Token  ----------------------------");
            //Console.WriteLine(apigeeTests.ApigeeToken());
            Console.WriteLine(" ------------------------  Ship-Notice  ----------------------------");
            //Console.WriteLine(apigeeTests.ShipNotices());
            Console.ReadLine();
        }
    }
}
