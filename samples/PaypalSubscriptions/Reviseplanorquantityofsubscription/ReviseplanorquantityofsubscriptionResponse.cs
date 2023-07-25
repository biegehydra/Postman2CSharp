using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PaypalSubscriptions
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<RevisePlanOrQuantityOfSubscriptionResponse>(myJsonResponse);
    public class RevisePlanOrQuantityOfSubscriptionResponse
    {
        [JsonPropertyName("plan_id")]
        public string PlanId { get; set; }

        [JsonPropertyName("shipping_amount")]
        public ShippingAmount ShippingAmount { get; set; }

        [JsonPropertyName("shipping_address")]
        public ShippingAddress ShippingAddress { get; set; }

        [JsonPropertyName("plan_overridden")]
        public bool PlanOverridden { get; set; }
        public List<Links> Links { get; set; }
    }
}