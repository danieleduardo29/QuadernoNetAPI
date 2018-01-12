using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuadernoNetAPI
{
    public class QInvoiceItem : QuadernoBase
    {
        [JsonProperty("description")]
        public string Description { get; set; } //Required

        [JsonProperty("quantity")]
        public decimal Quantity { get; set; }

        [JsonProperty("unit_price", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public decimal UnitPrice { get; set; } //Required

        [JsonProperty("total_amount", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public decimal TotalAmount { get; set; } //Required if UnitPrice not present

        [JsonProperty("discount_rate")]
        public decimal DiscountRate { get; set; }

        [JsonProperty("tax_1_name")]
        public string Tax1Name { get; set; }

        [JsonProperty("tax_1_rate")]
        public decimal Tax1Rate { get; set; }

        [JsonProperty("tax_1_country")]
        public string Tax1Country { get; set; }

        [JsonProperty("tax_2_name")]
        public string Tax2Name { get; set; }

        [JsonProperty("tax_2_rate")]
        public decimal Tax2Rate { get; set; }

        [JsonProperty("tax_2_country")]
        public string Tax2Country { get; set; }

        /// <summary>
        /// Used to reference an existing item.
        /// When a item is added to an invoice it's not necessary to fill the other attributes if the Reference is present
        /// </summary>
        [JsonProperty("reference")]
        public string Reference { get; set; }

        [JsonProperty("_destroy")]
        public int Destroy { get; set; } //ToDo: Set to 1 in order to remove an item from an invoice before calling Update
    }
}
