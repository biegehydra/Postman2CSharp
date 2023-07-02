using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<ListtransactionsforsubscriptionResponse>(myJsonResponse);
namespace PaypalSubscriptions
{
    public class ListtransactionsforsubscriptionResponse
    {
        public List<Transactions> Transactions { get; set; }
        public List<Links> Links { get; set; }
    }

    public class Transactions
    {
        public string Status { get; set; }
        public string Id { get; set; }
        public AmountWithBreakdown AmountWithBreakdown { get; set; }
        public PayerName PayerName { get; set; }
        public string PayerEmail { get; set; }
        public DateTime Time { get; set; }
    }

    public class AmountWithBreakdown
    {
        public GrossAmount GrossAmount { get; set; }
        public FeeAmount FeeAmount { get; set; }
        public NetAmount NetAmount { get; set; }
    }

    public class FeeAmount
    {
        public string CurrencyCode { get; set; }
        public string Value { get; set; }
    }

    public class GrossAmount
    {
        public string CurrencyCode { get; set; }
        public string Value { get; set; }
    }

    public class NetAmount
    {
        public string CurrencyCode { get; set; }
        public string Value { get; set; }
    }

    public class PayerName
    {
        public string GivenName { get; set; }
        public string Surname { get; set; }
    }
}