using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<UpdatesubscriptionRequest>>(myJsonResponse);
namespace PaypalSubscriptions
{
    public class UpdatesubscriptionRequest
    {
        public string Op { get; set; }
        public string Path { get; set; }
        public object Value { get; set; }
    }
}