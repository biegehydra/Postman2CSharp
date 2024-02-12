using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NewRelicAlerts
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<AlertsMutingRuleCreateGraphQLVariables>(myJsonResponse);
    public class AlertsMutingRuleCreateGraphQLVariables
    {
        [JsonPropertyName("accountId")]
        public int AccountId { get; set; }

        [JsonPropertyName("rule")]
        public Rule Rule { get; set; }
    }

    public class Schedule
    {
        [JsonPropertyName("endRepeat")]
        public string EndRepeat { get; set; }

        [JsonPropertyName("endTime")]
        public string EndTime { get; set; }

        [JsonPropertyName("repeat")]
        public string Repeat { get; set; }

        [JsonPropertyName("repeatCount")]
        public int RepeatCount { get; set; }

        [JsonPropertyName("startTime")]
        public string StartTime { get; set; }

        [JsonPropertyName("timeZone")]
        public string TimeZone { get; set; }

        [JsonPropertyName("weeklyRepeatDays")]
        public List<string> WeeklyRepeatDays { get; set; }
    }

    public class Rule
    {
        [JsonPropertyName("condition")]
        public Condition Condition { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("enabled")]
        public bool Enabled { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("schedule")]
        public Schedule Schedule { get; set; }
    }

    public class Conditions
    {
        [JsonPropertyName("attribute")]
        public string Attribute { get; set; }

        [JsonPropertyName("operator")]
        public string Operator { get; set; }

        [JsonPropertyName("values")]
        public List<string> Values { get; set; }
    }

    public class Condition
    {
        [JsonPropertyName("conditions")]
        public Conditions Conditions { get; set; }

        [JsonPropertyName("operator")]
        public string Operator { get; set; }
    }
}