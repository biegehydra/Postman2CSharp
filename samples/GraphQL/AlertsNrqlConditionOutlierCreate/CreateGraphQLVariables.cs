using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NewRelicAlerts
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<CreateGraphQLVariables>(myJsonResponse);
    public class CreateGraphQLVariables
    {
        [JsonPropertyName("accountId")]
        public int AccountId { get; set; }

        [JsonPropertyName("condition")]
        public Condition3 Condition { get; set; }

        [JsonPropertyName("policyId")]
        public int PolicyId { get; set; }
    }

    public class Condition3
    {
        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("enabled")]
        public bool Enabled { get; set; }

        [JsonPropertyName("expectedGroups")]
        public int ExpectedGroups { get; set; }

        [JsonPropertyName("expiration")]
        public Expiration Expiration { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("nrql")]
        public Nrql Nrql { get; set; }

        [JsonPropertyName("openViolationOnGroupOverlap")]
        public bool OpenViolationOnGroupOverlap { get; set; }

        [JsonPropertyName("runbookUrl")]
        public string RunbookUrl { get; set; }

        [JsonPropertyName("signal")]
        public Signal Signal { get; set; }

        [JsonPropertyName("terms")]
        public Terms Terms { get; set; }

        [JsonPropertyName("violationTimeLimit")]
        public string ViolationTimeLimit { get; set; }

        [JsonPropertyName("violationTimeLimitSeconds")]
        public string ViolationTimeLimitSeconds { get; set; }
    }
}