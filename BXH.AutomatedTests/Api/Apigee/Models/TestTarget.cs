using System.Collections.Generic;

namespace BXH.AutomatedTests.Api.Apigee.Models
{
    public class TestTarget
    {
        public string Name { get; set; }
        public int ProductAppID { get; set; }
        public string TargetURL { get; set; }
        public string HTTPVerb { get; set; }
        public List<TestTargetHeaders> Headers { get; set; }
        public List<TestTargetParameters> Parameters { get; set; }
    }
}