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

                //Create a Contact
                QContact contact = new QContact
                {
                    ContactName = "John Doe",
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@lost-company.com",
                    Kind = QContactKind.person
                };

                try
                {
                    contact.Save();

                    Console.WriteLine("El Id del contacto creado es: " + contact.Id);

                    List<QContact> contacts = QContact.Find();
                    Console.WriteLine("Retrieved contacts: " + contacts.Count());

                    foreach(QContact c in contacts)
                    {
                        Console.WriteLine("Deleting contact: " + c.Id);
                        c.Delete();
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Error executing some operation: " + ex.Message);
                }
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
