

using System.Net;
using BXH.AutomatedTests.Api.Apigee.Data;
using BXH.AutomatedTests.Api.Configs;
using RestSharp;

namespace BXH.AutomatedTests.Api.Apigee
{
    public class ApigeeProxyTests
    {
        public string token;
        public ApigeeTokenCredentials tokenCreds = new ApigeeTokenCredentials() { clientID = "Gs5GlMwUPpWo1HgRyODQjk6uZUCVNUIl", client_secret = "NyDLtEbGXijFNOXo" };

        public ApigeeProxyTests()
        {

        }

        public string ApigeeToken()
        {

            RestClient client = new RestClient(Dev.APIGEE_HOST_URL);

            var request = new RestRequest(Dev.APIGEE_RESOURCE_TOKEN, Method.POST);
            request.AddQueryParameter("grant_type", "client_credentials");
            request.AddParameter(
                                "application/x-www-form-urlencoded", 
                                $"client_id={tokenCreds.clientID}&client_secret={tokenCreds.client_secret}", 
                                ParameterType.RequestBody);

            var tokenResponse = client.Execute<ApigeeTokenResponse>(request);

            if (tokenResponse.StatusCode == HttpStatusCode.OK)
            {
                token = tokenResponse.Data.access_token;
                return $"SUCCESS: Status: {tokenResponse.StatusCode}, Token: {tokenResponse.Data.access_token}";
            }

            return $"FAILURE: {tokenResponse.StatusCode} : {tokenResponse.ErrorMessage}";
        }

        public string ShipNotices()
        {
            RestClient client = new RestClient(Dev.APIGEE_HOST_URL);

            var request = new RestRequest(Dev.APIGEE_RESOURCE_SHIPNOTICE, Method.POST);
            request.AddHeader("api-key", tokenCreds.clientID);
            request.AddHeader("Authorization", $"Bearer {token}");
            request.AddHeader("Content-Type", "application/xml");
            request.AddXmlBody(NoticeData.ShipNoticeXML);

            var res = client.Execute(request);

            if (res.StatusCode == HttpStatusCode.OK)
            {
                return $"SHIP NOTICE SUCCESS: Status: {res.StatusCode}, Token: {res.Content}";
            }

            return $"SHIP NOTICE endpoint failed: {res.StatusCode} : {res.ErrorMessage}";
        }
    }
}