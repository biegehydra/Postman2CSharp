using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<ListTransactionsForSubscriptionResponse>(myJsonResponse);
namespace PaypalSubscriptions
{
    public class ListTransactionsForSubscriptionResponse
    {
        public List<Transactions> Transactions { get; set; }
        public List<Links> Links { get; set; }
    }

    public class Transactions
    {
        public string Status { get; set; }
        public string Id { get; set; }
        public AmountWithBreakdown AmountWithBreakdown { get; set; }
        public Name PayerName { get; set; }
        public string PayerEmail { get; set; }
        public DateTime Time { get; set; }
    }

    public class AmountWithBreakdown
    {
        public ShippingAmount GrossAmount { get; set; }
        public ShippingAmount FeeAmount { get; set; }
        public ShippingAmount NetAmount { get; set; }
    }
}