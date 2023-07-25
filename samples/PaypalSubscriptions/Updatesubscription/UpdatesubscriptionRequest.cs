using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PaypalSubscriptions
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<List<UpdateSubscriptionRequest>>(myJsonResponse);
    public class UpdateSubscriptionRequest
    {
        public string Op { get; set; }
        public string Path { get; set; }
        public object Value { get; set; }
    }
}