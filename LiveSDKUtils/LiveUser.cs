using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LiveSDKUtils
{
    public static class LiveUser
    {
        private static string _uri = "https://login.live.com/oauth20_authorize.srf";
        private static string _clientID = "000000004C12BD17";
        public static void LiveSignin()
        {
            var authorizeUri = new StringBuilder(_uri);
            authorizeUri.AppendFormat("?client_id={0}&", _clientID);
            authorizeUri.AppendFormat("scope={0}&", "wl.signin");
            authorizeUri.AppendFormat("response_type={0}", "token");
            authorizeUri.AppendFormat("redirect_uri{0}", UpperCaseUrlEncode("http://localhost:61122/liveuser/ShowCode"));

            var startInfo = new ProcessStartInfo();
            startInfo.FileName = authorizeUri.ToString();
            Process.Start(startInfo);
        }
        private static string UpperCaseUrlEncode(string s)
        {
            char[] temp = HttpUtility.UrlEncode(s).ToCharArray();
            for (int i = 0; i < temp.Length - 2; i++)
            {
                if (temp[i] == '%')
                {
                    temp[i + 1] = char.ToUpper(temp[i + 1]);
                    temp[i + 2] = char.ToUpper(temp[i + 2]);
                }
            }

            var values = new Dictionary<string, string>()
            {
                { "+", "%20" },
                { "(", "%28" },
                { ")", "%29" }
            };

            var data = new StringBuilder(new string(temp));
            foreach (string character in values.Keys)
            {
                data.Replace(character, values[character]);
            }
            return data.ToString();
        }

        public static User GetUser()
        {
            var requestUri = new StringBuilder("https://apis.live.net/v5.0/me");
            requestUri.AppendFormat("?access_token={0}", "your access token");
            var request = (HttpWebRequest)WebRequest.Create(requestUri.ToString());
            request.Method = "GET";

            var response = (HttpWebResponse)request.GetResponse();
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                var json = reader.ReadToEnd();

                var user = JsonConvert.DeserializeObject<User>(json);

                return user;
            }
        }
    }
}
