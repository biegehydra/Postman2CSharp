using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NewRelicAlerts
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<AlertsPolicyCreateGraphQLVariables>(myJsonResponse);
    public class AlertsPolicyCreateGraphQLVariables
    {
        [JsonPropertyName("accountId")]
        public int AccountId { get; set; }

        [JsonPropertyName("policy")]
        public Policy Policy { get; set; }
    }

    public class Policy
    {
        [JsonPropertyName("incidentPreference")]
        public string IncidentPreference { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}