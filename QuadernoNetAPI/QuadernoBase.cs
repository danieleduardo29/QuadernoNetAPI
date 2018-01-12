using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections;
using Newtonsoft.Json;


namespace QuadernoNetAPI
{
    public class QuadernoBase
    {
        private static string API_KEY = null;
        private static string API_URL = null;
        private static string API_VERSION = null;

        private static HttpClient CLIENT = new HttpClient();

        [JsonProperty("id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public long Id { get; set; }

        public static void Init(string apiKey, string apiUrl, string apiVersion = null)
        {
            API_KEY = apiKey;
            API_URL = apiUrl;
            API_VERSION = apiVersion;

            CLIENT.BaseAddress = new Uri(API_URL);
            var byteArray = new UTF8Encoding().GetBytes(API_KEY);
            CLIENT.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
        }

        public static bool Ping()
        {
            using (HttpResponseMessage response = CLIENT.GetAsync("ping.json").Result)
            {
                if (((int)response.StatusCode) == 200)
                {
                    return true;
                }
                else
                {
                    //Console.WriteLine(content.ReadAsStringAsync().Result); // Display the response text
                    return false;
                }
            }
        }

        public static void Post(string url, QuadernoBase obj)
        {
            string jsonObj = JsonConvert.SerializeObject(obj);

            using(HttpResponseMessage response = CLIENT.PostAsJsonAsync(
                url, obj).Result)
            {
                response.EnsureSuccessStatusCode();

                string result = response.Content.ReadAsStringAsync().Result;
                QuadernoBase responseObj = JsonConvert.DeserializeObject<QuadernoBase>(result);
                obj.Id = responseObj.Id;
            }
        }

        public static void Put(string url, QuadernoBase obj)
        {
            using (HttpResponseMessage response = CLIENT.PutAsJsonAsync(
                url, obj).Result)
            {
                response.EnsureSuccessStatusCode();
            }
        }

        public static string Get(string url)
        {
            using (HttpResponseMessage response = CLIENT.GetAsync(
                url).Result)
            {
                response.EnsureSuccessStatusCode();

                return response.Content.ReadAsStringAsync().Result;
            }
        }

        public static void Delete(string url)
        {
            using (HttpResponseMessage response = CLIENT.DeleteAsync(
                url).Result)
            {
                response.EnsureSuccessStatusCode();
            }
        }
    }
}
