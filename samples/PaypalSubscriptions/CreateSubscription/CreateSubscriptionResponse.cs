using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PaypalSubscriptions
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<CreateSubscriptionResponse>(myJsonResponse);
    public class CreateSubscriptionResponse
    {
        public string Status { get; set; }

        [JsonPropertyName("status_update_time")]
        public DateTime StatusUpdateTime { get; set; }
        public string Id { get; set; }

        [JsonPropertyName("plan_id")]
        public string PlanId { get; set; }

        [JsonPropertyName("start_time")]
        public DateTime StartTime { get; set; }
        public string Quantity { get; set; }
        public PaymentMethod Subscriber { get; set; }

        [JsonPropertyName("create_time")]
        public DateTime CreateTime { get; set; }

        [JsonPropertyName("plan_overridden")]
        public bool PlanOverridden { get; set; }
        public List<Links> Links { get; set; }
    }

    public class Links
    {
        public string Href { get; set; }
        public string Rel { get; set; }
        public string Method { get; set; }
    }
}