using RestSharp;

namespace BXH.AutomatedTests.Api.Models
{
    public class TestResult
    {
        public string TestName { get; set; }
        public string Status { get; set; }
        public IRestResponse Response { get; set; }
    }
}