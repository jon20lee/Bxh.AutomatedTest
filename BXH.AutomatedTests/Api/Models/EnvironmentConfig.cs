using System.Collections.Generic;

namespace BXH.AutomatedTests.Api.Models
{
    public class EnvironmentConfig
    {
        public string Name { get; set; }
        public string HostURL { get; set; }
        public List<ProductApp> ProductApps { get; set; }
        public TestTargetCredentials credentials { get; set; }
    }
}