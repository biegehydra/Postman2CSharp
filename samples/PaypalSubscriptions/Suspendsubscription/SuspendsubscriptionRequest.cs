using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PaypalSubscriptions
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<SuspendSubscriptionRequest>(myJsonResponse);
    public class SuspendSubscriptionRequest
    {
        public string Reason { get; set; }
    }
}