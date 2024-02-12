using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NewRelicAlerts
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<AlertsMutingRuleUpdateGraphQLVariables>(myJsonResponse);
    public class AlertsMutingRuleUpdateGraphQLVariables
    {
        [JsonPropertyName("accountId")]
        public int AccountId { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("rule")]
        public Rule Rule { get; set; }
    }
}