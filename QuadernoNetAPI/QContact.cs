using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuadernoNetAPI
{
    public class QContact : QuadernoBase
    {
        [JsonProperty("kind"), JsonConverter(typeof(StringEnumConverter))]
        public QContactKind Kind { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; } //ToDo: validate --> required
        
        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("contact_name")]
        public string ContactName { get; set; }

        [JsonProperty("street_line_1")]
        public string StreetLine1 { get; set; }

        [JsonProperty("street_line_2")]
        public string StreetLine2 { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("postal_code")]
        public string PostalCode { get; set; }

        [JsonProperty("region", NullValueHandling = NullValueHandling.Ignore)]
        public string Region { get; set; }

        [JsonProperty("Country", NullValueHandling = NullValueHandling.Ignore)]
        public string Country { get; set; } //ToDo: restrict to the accepted values

        //Address 2 fields missing

        [JsonProperty("phone_1")]
        public string Phone1 { get; set; }

        [JsonProperty("phone_2")]
        public string Phone2 { get; set; }

        [JsonProperty("fax")]
        public string Fax { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("web")]
        public string Web { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; } //ToDo: restrict to the accepted values

        [JsonProperty("tax_id")]
        public string TaxId { get; set; }

        [JsonProperty("vat_number")]
        public string VatNumber { get; set; }

        [JsonProperty("bank_account")]
        public string BankAccount { get; set; }

        [JsonProperty("bic")]
        public string Bic { get; set; } //ToDo: validate --> required if BankAccount is present

        [JsonProperty("notes")]
        public string Notes { get; set; }


        public void Save()
        {
            if(Id == 0)
            {
                Post("contacts.json", this);
            }
            else
            {
                Put(string.Format("contacts/{0}.json", Id), this);
            }
            
        }

        public static List<QContact> Find(string query = null)
        {
            string result = Get(string.Format("contacts.json{0}", 
                                                query == null ? "" : 
                                                string.Format("?q={0}", query)));

            //Console.WriteLine(result);

            return JsonConvert.DeserializeObject<List<QContact>>(result);
        }

        public static QContact Find(long contactId)
        {
            string result = Get(string.Format("contacts/{0}.json", contactId));

            //Console.WriteLine(result);

            return JsonConvert.DeserializeObject<QContact>(result);
        }

        public void Delete()
        {
            Delete(string.Format("contacts/{0}.json", Id));
            Id = 0;
        }
    }

    public enum QContactKind
    {
        company = 0,
        person = 1
    }
}
