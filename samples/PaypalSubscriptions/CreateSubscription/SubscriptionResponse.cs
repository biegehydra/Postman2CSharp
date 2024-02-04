using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PaypalSubscription
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<SubscriptionResponse>(myJsonResponse);
    public class SubscriptionResponse
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("status_update_time")]
        public DateTime StatusUpdateTime { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("plan_id")]
        public string PlanId { get; set; }

        [JsonPropertyName("start_time")]
        public DateTime StartTime { get; set; }

        [JsonPropertyName("quantity")]
        public string Quantity { get; set; }

        [JsonPropertyName("subscriber")]
        public PaymentMethod Subscriber { get; set; }

        [JsonPropertyName("create_time")]
        public DateTime CreateTime { get; set; }

        [JsonPropertyName("plan_overridden")]
        public bool PlanOverridden { get; set; }

        [JsonPropertyName("links")]
        public List<Links> Links { get; set; }
    }

    public class Links
    {
        [JsonPropertyName("href")]
        public string Href { get; set; }

        [JsonPropertyName("rel")]
        public string Rel { get; set; }

        [JsonPropertyName("method")]
        public string Method { get; set; }
    }
}