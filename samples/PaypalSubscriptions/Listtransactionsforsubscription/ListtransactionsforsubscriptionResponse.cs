using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PaypalSubscriptions
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<ListTransactionsForSubscriptionResponse>(myJsonResponse);
    public class ListTransactionsForSubscriptionResponse
    {
        public List<Transactions> Transactions { get; set; }
        public List<Links> Links { get; set; }
    }

    public class Transactions
    {
        public string Status { get; set; }
        public string Id { get; set; }

        [JsonPropertyName("amount_with_breakdown")]
        public AmountWithBreakdown AmountWithBreakdown { get; set; }

        [JsonPropertyName("payer_name")]
        public Name PayerName { get; set; }

        [JsonPropertyName("payer_email")]
        public string PayerEmail { get; set; }
        public DateTime Time { get; set; }
    }

    public class AmountWithBreakdown
    {
        [JsonPropertyName("gross_amount")]
        public ShippingAmount GrossAmount { get; set; }

        [JsonPropertyName("fee_amount")]
        public ShippingAmount FeeAmount { get; set; }

        [JsonPropertyName("net_amount")]
        public ShippingAmount NetAmount { get; set; }
    }
}