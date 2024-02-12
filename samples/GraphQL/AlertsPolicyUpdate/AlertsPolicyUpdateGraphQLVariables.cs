using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NewRelicAlerts
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<AlertsPolicyUpdateGraphQLVariables>(myJsonResponse);
    public class AlertsPolicyUpdateGraphQLVariables
    {
        [JsonPropertyName("accountId")]
        public int AccountId { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("policy")]
        public Policy Policy { get; set; }
    }
}