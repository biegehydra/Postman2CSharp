using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<ActivatesubscriptionRequest>(myJsonResponse);
namespace PaypalSubscriptions
{
    public class ActivatesubscriptionRequest
    {
        public string Reason { get; set; }
    }
}