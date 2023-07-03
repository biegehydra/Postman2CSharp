using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<CancelSubscriptionRequest>(myJsonResponse);
namespace PaypalSubscriptions
{
    public class CancelSubscriptionRequest
    {
        public string Reason { get; set; }
    }
}