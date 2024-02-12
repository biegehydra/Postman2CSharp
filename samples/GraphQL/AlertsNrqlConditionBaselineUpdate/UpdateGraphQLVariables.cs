using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NewRelicAlerts
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<UpdateGraphQLVariables>(myJsonResponse);
    public class UpdateGraphQLVariables
    {
        [JsonPropertyName("accountId")]
        public int AccountId { get; set; }

        [JsonPropertyName("condition")]
        public Condition2 Condition { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }
    }

    public class Condition2
    {
        [JsonPropertyName("baselineDirection")]
        public string BaselineDirection { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("enabled")]
        public bool Enabled { get; set; }

        [JsonPropertyName("expiration")]
        public Expiration Expiration { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("nrql")]
        public Nrql Nrql { get; set; }

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

    public class Signal
    {
        [JsonPropertyName("aggregationDelay")]
        public string AggregationDelay { get; set; }

        [JsonPropertyName("aggregationMethod")]
        public string AggregationMethod { get; set; }

        [JsonPropertyName("aggregationTimer")]
        public string AggregationTimer { get; set; }

        [JsonPropertyName("aggregationWindow")]
        public string AggregationWindow { get; set; }

        [JsonPropertyName("evaluationOffset")]
        public int EvaluationOffset { get; set; }

        [JsonPropertyName("fillOption")]
        public string FillOption { get; set; }

        [JsonPropertyName("fillValue")]
        public string FillValue { get; set; }

        [JsonPropertyName("slideBy")]
        public string SlideBy { get; set; }
    }

    public class Terms
    {
        [JsonPropertyName("operator")]
        public string Operator { get; set; }

        [JsonPropertyName("priority")]
        public string Priority { get; set; }

        [JsonPropertyName("threshold")]
        public string Threshold { get; set; }

        [JsonPropertyName("thresholdDuration")]
        public int ThresholdDuration { get; set; }

        [JsonPropertyName("thresholdOccurrences")]
        public string ThresholdOccurrences { get; set; }
    }

    public class Expiration
    {
        [JsonPropertyName("closeViolationsOnExpiration")]
        public bool CloseViolationsOnExpiration { get; set; }

        [JsonPropertyName("expirationDuration")]
        public string ExpirationDuration { get; set; }

        [JsonPropertyName("openViolationOnExpiration")]
        public bool OpenViolationOnExpiration { get; set; }
    }

    public class Nrql
    {
        [JsonPropertyName("evaluationOffset")]
        public int EvaluationOffset { get; set; }

        [JsonPropertyName("query")]
        public string Query { get; set; }
    }
}