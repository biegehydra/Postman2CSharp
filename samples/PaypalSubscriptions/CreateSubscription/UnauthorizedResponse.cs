using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PaypalSubscription
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<UnauthorizedResponse>(myJsonResponse);
    public class UnauthorizedResponse
    {
        [JsonPropertyName("error")]
        public string Error { get; set; }

        [JsonPropertyName("error_description")]
        public string ErrorDescription { get; set; }
    }
}