using System;
using System.Collections.Generic;
using System.Text.Json;

// Root myDeserializedClass = JsonSerializer.Deserialize<ActivatesubscriptionRequest>(myJsonResponse);
namespace PaypalSubscriptions
{
    public class ActivatesubscriptionRequest
    {
        public string Reason { get; set; }
    }
}