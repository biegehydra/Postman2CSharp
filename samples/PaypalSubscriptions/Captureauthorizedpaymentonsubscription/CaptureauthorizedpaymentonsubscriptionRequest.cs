using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<CaptureauthorizedpaymentonsubscriptionRequest>(myJsonResponse);
namespace PaypalSubscriptions
{
    public class CaptureauthorizedpaymentonsubscriptionRequest
    {
        public string Note { get; set; }
        public string CaptureType { get; set; }
        public Amount Amount { get; set; }
    }

    public class Amount
    {
        public string CurrencyCode { get; set; }
        public string Value { get; set; }
    }
}