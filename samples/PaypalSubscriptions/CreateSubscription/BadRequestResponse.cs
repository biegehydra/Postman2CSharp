using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PaypalSubscriptions
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<BadRequestResponse>(myJsonResponse);
    public class BadRequestResponse
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("debug_id")]
        public string DebugId { get; set; }

        [JsonPropertyName("details")]
        public List<Details> Details { get; set; }

        [JsonPropertyName("links")]
        public List<Links> Links { get; set; }
    }

    public class Details
    {
        [JsonPropertyName("field")]
        public string Field { get; set; }

        [JsonPropertyName("value")]
        public DateTime Value { get; set; }

        [JsonPropertyName("location")]
        public string Location { get; set; }

        [JsonPropertyName("issue")]
        public string Issue { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}