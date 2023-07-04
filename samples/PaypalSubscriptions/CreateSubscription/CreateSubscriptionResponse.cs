using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<CreateSubscriptionResponse>(myJsonResponse);
namespace PaypalSubscriptions
{
    public class CreateSubscriptionResponse
    {
        public string Status { get; set; }
        public DateTime StatusUpdateTime { get; set; }
        public string Id { get; set; }
        public string PlanId { get; set; }
        public DateTime StartTime { get; set; }
        public string Quantity { get; set; }
        public Subscriber Subscriber { get; set; }
        public DateTime CreateTime { get; set; }
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