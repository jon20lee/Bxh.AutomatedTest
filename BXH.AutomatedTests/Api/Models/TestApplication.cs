using System.Collections.Generic;
using RestSharp;

namespace BXH.AutomatedTests.Api.Models
{
    public class TestApplication
    {
        public string Name { get; set; }
        public RestClient Client { get; set; }
        public List<TestTarget> Targets;
        public List<EnvironmentConfig> Environments { get; set; }
    }
}