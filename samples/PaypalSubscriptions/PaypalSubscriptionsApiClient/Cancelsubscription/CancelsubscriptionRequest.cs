using System;
using System.Collections.Generic;
using System.Text.Json;

// Root myDeserializedClass = JsonSerializer.Deserialize<CancelsubscriptionRequest>(myJsonResponse);
namespace PaypalSubscriptions
{
    public class CancelsubscriptionRequest
    {
        public string Reason { get; set; }
    }
}