using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<RevisePlanOrQuantityOfSubscriptionRequest>(myJsonResponse);
namespace PaypalSubscriptions
{
    public class RevisePlanOrQuantityOfSubscriptionRequest
    {
        public string PlanId { get; set; }
        public ShippingAmount ShippingAmount { get; set; }
        public ShippingAddress ShippingAddress { get; set; }
        public ApplicationContext ApplicationContext { get; set; }
    }
}