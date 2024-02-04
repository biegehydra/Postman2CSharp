using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PaypalSubscription
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<RevisePlanOrQuantityOfSubscriptionRequest>(myJsonResponse);
    public class RevisePlanOrQuantityOfSubscriptionRequest
    {
        [JsonPropertyName("plan_id")]
        public string PlanId { get; set; }

        [JsonPropertyName("shipping_amount")]
        public ShippingAmount ShippingAmount { get; set; }

        [JsonPropertyName("shipping_address")]
        public ShippingAddress ShippingAddress { get; set; }

        [JsonPropertyName("application_context")]
        public ApplicationContext ApplicationContext { get; set; }
    }
}