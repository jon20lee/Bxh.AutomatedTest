using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using BXH.AutomatedTests.Api.Models;
using RestSharp;
using Serilog.Core;

namespace BXH.AutomatedTests.Api.Apigee
{
    public class ApigeeProxyTests
    {

        private TestHelper testHelper;
        private Logger _logger;

        public ApigeeProxyTests(TestHelper conf)
        {
            testHelper = conf;
            _logger = conf.Logger;
        }

        public string ApigeeToken(string clientId, string clientSecret)
        {
            var request = new RestRequest("/oauth/client_credential/accesstoken", Method.POST);
            request.AddQueryParameter("grant_type", "client_credentials");
            request.AddParameter(
                "application/x-www-form-urlencoded",
                $"client_id={clientId}&client_secret={clientSecret}",
                ParameterType.RequestBody);

            var tokenResponse = testHelper.Client.Execute<ApigeeTokenResponse>(request);

            if (tokenResponse.StatusCode == HttpStatusCode.OK)
            {
                return tokenResponse.Data.access_token;
            }

            return "";
        }

        public string ShipNotices()
        {
            var res = testHelper.RunTests("AdvancedShipNotice", ApigeeToken(testHelper.configs.ProductApps.FirstOrDefault().ClientID, testHelper.configs.ProductApps.FirstOrDefault().ClientSecret));

            if (res?.FirstOrDefault()?.Response.StatusCode == HttpStatusCode.OK)
            {
                return $"SHIP NOTICE SUCCESS: Status: {res?.FirstOrDefault()?.Response.StatusCode}";
            }

            return $"SHIP NOTICE endpoint failed: {res?.FirstOrDefault()?.Response.StatusCode} : {res?.FirstOrDefault()?.Response.ErrorMessage}";
        }

        public string BulkShipStatus()
        {
            var res = testHelper.RunTests("BulkShipStatus", ApigeeToken(testHelper.configs.ProductApps.FirstOrDefault().ClientID, testHelper.configs.ProductApps.FirstOrDefault().ClientSecret));

            if (res?.FirstOrDefault()?.Response.StatusCode == HttpStatusCode.OK)
            {
                return $"BULK SHIP NOTICE SUCCESS: Status: {res?.FirstOrDefault()?.Response.StatusCode}";
            }

            return $"BULK SHIP NOTICE endpoint failed: {res?.FirstOrDefault()?.Response.StatusCode} : {res?.FirstOrDefault()?.Response.ErrorMessage}";
        }
    }
}