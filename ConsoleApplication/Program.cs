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

                //CallSomeContactMethods();

                QInvoice invoice = new QInvoice
                {
                    Currency = "EUR",
                    IssueDate = DateTime.Now,
                    //DueDate = DateTime.Now,
                    PaymentMethod = QPaymentMethod.credit_card //When it's set it will register a payment and the Invoice state will be paid
                };

                QInvoiceItem item = new QInvoiceItem
                {
                    Description = "Coffee",
                    UnitPrice = 3.2m,
                    Quantity = 2
                };

                invoice.Items.Add(item);

                invoice.Contact = new QContact
                {
                    FirstName = "Bruce",
                    LastName = "Wayne",
                    Kind = QContactKind.person,
                    ContactName = "Batman",
                    Email = "ceo@wayne-enterprises.com"
                };

                invoice.Save();

                Console.WriteLine(invoice.Pdf);
                
                var invoices = QInvoice.Find();

                foreach(QInvoice inv in invoices)
                {
                    inv.Delete();
                }
            }
            else
            {
                Console.WriteLine("Error establishing connection. Be sure that you added the right Key and URL on App.config");
            }

            Console.WriteLine("\nPress a key to exit");
            Console.ReadKey();
        }

        private static void CallSomeContactMethods()
        {
            //Create a Contact
            QContact newContact = new QContact
            {
                ContactName = "John Doe",
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@lost-company.com",
                Kind = QContactKind.person
            };

            try
            {
                newContact.Save();

                Console.WriteLine("The new contact Id is: " + newContact.Id);

                List<QContact> contacts = QContact.Find();
                Console.WriteLine("Retrieved contacts: " + contacts.Count());

                Console.WriteLine("Deleting newContact");
                newContact.Delete();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error executing some operation with QContacts: " + ex.Message);
            }
        }
    }
}
