using System.Collections.Generic;

namespace BXH.AutomatedTests.Api.Apigee
{
    public class ApigeeTokenResponse
    {
        public string refresh_token_expires_in { get; set; }
        public string api_product_list { get; set; }
        public List<string> api_product_list_json { get; set; }
        public string organization_name { get; set; }
        public string token_type { get; set; }
        public string issued_at { get; set; }
        public string client_id { get; set; }
        public string access_token { get; set; }
        public string application_name { get; set; }
        public string scope { get; set; }
        public string expires_in { get; set; }
        public string refresh_count { get; set; }
        public string status { get; set; }
    }
}