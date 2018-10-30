using System.Collections.Generic;

namespace BXH.AutomatedTests.Api.Apigee.Models
{
    public class ApigeeConfig
    {
        public string HostURL { get; set; }
        public List<ApigeeProductApp> ProductApps { get; set; }
    }
}