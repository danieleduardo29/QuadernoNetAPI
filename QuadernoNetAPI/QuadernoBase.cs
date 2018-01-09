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
        private static string URL = null;

        public static void Init(string apiKey, string url)
        {
            API_KEY = apiKey;
            URL = url;
        }

        public static bool Ping()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://sandbox-quadernoapp.com/api/");
            var byteArray = new UTF8Encoding().GetBytes(API_KEY);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            
            HttpResponseMessage response = client.GetAsync("ping.json").Result;
            HttpContent content = response.Content;

            // ... Check Status Code                                
            Console.WriteLine("Response StatusCode: " + (int)response.StatusCode);

            // ... Read the string.
            string result = content.ReadAsStringAsync().Result;

            // ... Display the result.
            Console.WriteLine(result);
            
            return true;
        }
    }
}
