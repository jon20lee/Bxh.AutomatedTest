using System.Collections.Generic;

namespace BXH.AutomatedTests.Api.Models
{
    public class TestTargetTestCases
    {
        public int id { get; set; }
        public string name { get; set; }
        public List<int> paramId { get; set; }
        public int resultCode { get; set; }
        public int iterations { get; set; }
    }
}