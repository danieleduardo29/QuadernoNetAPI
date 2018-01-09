using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using System.Net.Http;
using System.Net.Http.Headers;


namespace QuadernoNetAPI
{
    public class QuadernoBase
    {
        private static string API_KEY = null;
        private static string API_URL = null;
        private static string API_VERSION = null;

        public static void Init(string apiKey, string apiUrl, string apiVersion = null)
        {
            API_KEY = apiKey;
            API_URL = apiUrl;
            API_VERSION = apiVersion;
        }

        public static bool Ping()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(API_URL.Contains("sandbox") ? 
                                                "http://sandbox-quadernoapp.com/api/" :
                                                "https://quadernoapp.com/api/");
            var byteArray = new UTF8Encoding().GetBytes(API_KEY);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            using (HttpResponseMessage response = client.GetAsync("ping.json").Result)
            {
                HttpContent content = response.Content;

                if (((int)response.StatusCode) == 200)
                {
                    return true;
                }
                else
                {
                    // ... Display the response text.
                    //Console.WriteLine(content.ReadAsStringAsync().Result);
                    return false;
                }
            }
        }


    }
}
