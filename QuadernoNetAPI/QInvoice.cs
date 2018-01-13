using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using QuadernoNetAPI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuadernoNetAPI
{
    public class QInvoice : QuadernoBase
    {
        [JsonProperty("number")]
        public string Number { get; set; }
        
        [JsonProperty("issue_date"), JsonConverter(typeof(OnlyDateConverter))]
        public DateTime IssueDate { get; set; }

        [JsonProperty("po_number")]
        public string PoNumber { get; set; }

        [JsonProperty("due_date", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(OnlyDateConverter))]
        public DateTime? DueDate { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; } // 3 chars (ISO 4217)

        //[JsonProperty("tag_list")]
        //public List<string> TagList { get; set; } //Tags separated by commas to save but as an array to retrieve

        [JsonProperty("payment_details")]
        public string PaymentDetails { get; set; }

        [JsonProperty("notes")]
        public string Notes { get; set; }

        [JsonProperty("contact_id")]
        public long ContactId { get; set; } //Mandatory if Contact is not present

        [JsonProperty("contact")]
        public QContact Contact { get; set; } //Mandatory if ContactId is not present

        [JsonProperty("country")]
        public string Country { get; set; } // 2 chars (ISO 3166-1 alpha-2)

        [JsonProperty("street_line_1")]
        public string StreetLine1 { get; set; }

        [JsonProperty("street_line_2")]
        public string StreetLine2 { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("postal_code")]
        public string PostalCode { get; set; }

        [JsonProperty("items_attributes")]
        public List<QInvoiceItem> Items { get; set; }

        [JsonProperty("payment_method", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(StringEnumConverter))]
        public QPaymentMethod? PaymentMethod { get; set; }

        //[JsonProperty("custom_metadata")]
        //public Dictionary<string,string> CustomMetadata { get; set; } //ToDo: do the JSON serializing right

#region READ-ONLY properties
        
        //ToDo: allow SET only by serializer

        [JsonProperty("exchange_rate", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public decimal ExchangeRate { get; set; }

        [JsonProperty("subtotal_cents", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public decimal SubtotalCents { get; set; }

        [JsonProperty("discount_cents", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public decimal DiscountCents { get; set; }

        //ToDo: Add Taxes attribute

        [JsonProperty("total_cents", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public decimal TotalCents { get; set; }

        //ToDo: Add Payments and PaymentDetails attributes

        [JsonProperty("secure_id", NullValueHandling = NullValueHandling.Ignore)]
        public string SecureId { get; set; }
        
        [JsonProperty("permalink", NullValueHandling = NullValueHandling.Ignore)]
        public string Permalink { get; set; }

        [JsonProperty("pdf", NullValueHandling = NullValueHandling.Ignore)]
        public string Pdf { get; set; }
#endregion


        public QInvoice()
        {
            Items = new List<QInvoiceItem>();
            IssueDate = DateTime.Now;
        }

        public void Save()
        {
            string result;
            if(Id == 0)
            {
                result = Post("invoices.json", this);
            }
            else
            {
                result = Put(string.Format("invoices/{0}.json", Id), this);
            }

            QInvoice aux = JsonConvert.DeserializeObject<QInvoice>(result);
            this.ContactId = aux.ContactId;
            this.DiscountCents = aux.DiscountCents;
            this.Pdf = aux.Pdf;
            this.Permalink = aux.Permalink;
            this.SecureId = aux.SecureId;
            this.SubtotalCents = aux.SubtotalCents;
            this.TotalCents = aux.TotalCents;
        }

        public static List<QInvoice> Find(string query = null)
        {
            string result = Get(string.Format("invoices.json{0}", 
                                                query == null ? "" : 
                                                string.Format("?q={0}", query)));

            //Console.WriteLine(result);

            return JsonConvert.DeserializeObject<List<QInvoice>>(result);
        }

        public static QInvoice Find(long contactId)
        {
            string result = Get(string.Format("invoices/{0}.json", contactId));

            //Console.WriteLine(result);

            return JsonConvert.DeserializeObject<QInvoice>(result);
        }

        public void Delete()
        {
            Delete(string.Format("invoices/{0}.json", Id));
            Id = 0;
        }

        public void Deliver()
        {
            string result = Get(string.Format("invoices/{0}/deliver.json", Id));

            //Console.WriteLine(result);
        }
    }

    public enum QInvoiceState
    {
        outstanding = 0,
        paid = 1,
        late = 2,
        archived = 3
    }

    public enum QPaymentMethod
    {
        credit_card = 0,
        cash = 1,
        wire_transfer = 2,
        direct_debit = 3,
        check = 4,
        promissory_note = 5,
        iou = 6,
        paypal = 7,
        other = 8
    }
}
