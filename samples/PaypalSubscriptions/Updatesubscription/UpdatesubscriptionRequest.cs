using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PaypalSubscriptions
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<List<UpdateSubscriptionRequest>>(myJsonResponse);
    public class UpdateSubscriptionRequest
    {
        [JsonPropertyName("op")]
        public string Op { get; set; }

        [JsonPropertyName("path")]
        public string Path { get; set; }

        [JsonPropertyName("value")]
        public object Value { get; set; }
    }
}