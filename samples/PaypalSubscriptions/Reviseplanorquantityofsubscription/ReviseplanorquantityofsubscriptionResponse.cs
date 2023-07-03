using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<RevisePlanOrQuantityOfSubscriptionResponse>(myJsonResponse);
namespace PaypalSubscriptions
{
    public class RevisePlanOrQuantityOfSubscriptionResponse
    {
        public string PlanId { get; set; }
        public ShippingAmount ShippingAmount { get; set; }
        public ShippingAddress ShippingAddress { get; set; }
        public bool PlanOverridden { get; set; }
        public List<Links> Links { get; set; }
    }
}