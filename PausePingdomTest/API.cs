using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace PausePingdomTest
{
    public class PingdomChecks
    {
        public List<PingdomCheck> Checks { get; set; }
    }

    public class PingdomCheck
    {
        public string ID { get; set; }
        public int Created { get; set; }
        public string Name { get; set; }
        public string Hostname { get; set; }
        public string Resolution { get; set; }
        public int LastErrorTime { get; set; }
        public int LastTestTime { get; set; }
        public int LastResponseTime { get; set; }
        public string Status { get; set; }
        public string[] Tags { get; set; }
    }

    public class Tools
    {

        public static void SetBasicAuthHeader(WebRequest request, String userName, String userPassword)
        {
            string authInfo = userName + ":" + userPassword;
            authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
            request.Headers["Authorization"] = "Basic " + authInfo;
        }

        public static void Pingdom()
        {
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create("https://api.pingdom.com/api/2.0/checks");
            request.Headers.Add("App-Key", "16p8z1mb5eeimf2pt7r2leiusesq0ylv");
            request.ContentLength = 0;
            request.PreAuthenticate = true;
            request.Method = "GET";
            SetBasicAuthHeader(request, "pingdom@think-team.com", "!1Th!nkP!ng123");

            HttpWebResponse response = (HttpWebResponse) request.GetResponse();
            var stream = response.GetResponseStream();
            var reader = new StreamReader(stream);
            var html = reader.ReadToEnd();

            PingdomCheck Checks = JsonConvert.DeserializeObject<PingdomCheck>(html);
        }
    }
}
