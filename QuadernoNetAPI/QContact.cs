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
        public string FirstName { get; set; }
        
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

        [JsonProperty("country", NullValueHandling = NullValueHandling.Ignore)]
        public string Country { get; set; } // 2 chars (ISO 3166-1 alpha-2)

        [JsonProperty("secondary_street_line_1")]
        public string SecondaryStreetLine1 { get; set; }

        [JsonProperty("secondary_street_line_2")]
        public string SecondaryStreetLine2 { get; set; }

        [JsonProperty("secondary_city")]
        public string SecondaryCity { get; set; }

        [JsonProperty("secondary_postal_code")]
        public string SecondaryPostalCode { get; set; }

        [JsonProperty("secondary_region", NullValueHandling = NullValueHandling.Ignore)]
        public string SecondaryRegion { get; set; }

        [JsonProperty("secondary_country", NullValueHandling = NullValueHandling.Ignore)]
        public string SecondaryCountry { get; set; } // 2 chars (ISO 3166-1 alpha-2)

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
        public string Language { get; set; }

        [JsonProperty("tax_id")]
        public string TaxId { get; set; }

        [JsonProperty("vat_number")]
        public string VatNumber { get; set; }

        [JsonProperty("bank_account")]
        public string BankAccount { get; set; }

        [JsonProperty("bic")]
        public string Bic { get; set; }

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
