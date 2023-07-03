using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<SuspendSubscriptionRequest>(myJsonResponse);
namespace PaypalSubscriptions
{
    public class SuspendSubscriptionRequest
    {
        public string Reason { get; set; }
    }
}