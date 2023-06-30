using System;
using System.Collections.Generic;
using System.Text.Json;

// Root myDeserializedClass = JsonSerializer.Deserialize<CaptureauthorizedpaymentonsubscriptionRequest>(myJsonResponse);
namespace PaypalSubscriptions
{
    public class Amount
    {
        public string CurrencyCode { get; set; }
        public string Value { get; set; }
    }

    public class CaptureauthorizedpaymentonsubscriptionRequest
    {
        public string Note { get; set; }
        public string CaptureType { get; set; }
        public Amount Amount { get; set; }
    }
}