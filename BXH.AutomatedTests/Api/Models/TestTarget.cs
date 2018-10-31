using System.Collections.Generic;
using BXH.AutomatedTests.Api.Models;

namespace BXH.AutomatedTests.Api.Models
{
    public class TestTarget
    {
        public string Name { get; set; }
        public int ProductAppID { get; set; }
        public string TargetURL { get; set; }
        public string HTTPVerb { get; set; }
        public List<TestTargetHeaders> Headers { get; set; }
        public List<TestTargetParameters> Parameters { get; set; }
        public List<TestTargetTestCases> TestCases { get; set; }
    }
}