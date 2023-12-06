using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PaypalSubscriptions
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<ReasonRequest>(myJsonResponse);
    public class ReasonRequest
    {
        [JsonPropertyName("reason")]
        public string Reason { get; set; }
    }
}