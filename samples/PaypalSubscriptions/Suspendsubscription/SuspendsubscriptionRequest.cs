using System;
using System.Collections.Generic;
using System.Text.Json;

// Root myDeserializedClass = JsonSerializer.Deserialize<SuspendsubscriptionRequest>(myJsonResponse);
namespace PaypalSubscriptions
{
    public class SuspendsubscriptionRequest
    {
        public string Reason { get; set; }
    }
}