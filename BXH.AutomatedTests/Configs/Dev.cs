namespace BXH.AutomatedTests.Configs
{
    public class Dev
    {

        //  --------------------------------------- APIGEE ---------------------------------------

        public static readonly string APIGEE_HOST_URL = "https://nutrien-nonprod-dev.apigee.net";
        
        public static readonly string APIGEE_RESOURCE_TOKEN = "/oauth/client_credential/accesstoken";
        public static readonly string APIGEE_RESOURCE_SHIPNOTICE = "/v1/suppliers/notices/advancedship";
        public static readonly string APIGEE_RESOURCE_BULKSHIPNOTICE = "/v1/suppliers/notices/bulkshipstatus";

        //  --------------------------------------- BXH ---------------------------------------


        //  --------------------------------------- CORE ---------------------------------------

        //  --------------------------------------- INNER ---------------------------------------

    }
}