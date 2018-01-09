using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QuadernoNetAPI;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            string privateKey = ConfigurationManager.AppSettings["QuadernoPrivateKey"];
            string publicKey = ConfigurationManager.AppSettings["QuadernoPublicKey"];
            string apiURL = ConfigurationManager.AppSettings["QuadernoURL"];

            QuadernoBase.Init(privateKey, apiURL);
            if(QuadernoBase.Ping())
            {
                Console.WriteLine("Connection established successfully");
            }
            else
            {
                Console.WriteLine("Error establishing connection. Be sure that you added the right Key and URL on App.config");
            }

            Console.WriteLine("\nPress a key to exit");
            Console.ReadKey();
        }
    }
}
