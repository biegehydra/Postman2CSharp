using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<ActivateSubscriptionRequest>(myJsonResponse);
namespace PaypalSubscriptions
{
    public class ActivateSubscriptionRequest
    {
        public string Reason { get; set; }
    }
}