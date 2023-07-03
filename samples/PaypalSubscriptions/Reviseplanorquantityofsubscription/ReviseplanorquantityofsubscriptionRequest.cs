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
        public ApplicationContext2 ApplicationContext { get; set; }
    }

    public class ApplicationContext2
    {
        public string BrandName { get; set; }
        public string Locale { get; set; }
        public string ShippingPreference { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string ReturnUrl { get; set; }
        public string CancelUrl { get; set; }
    }

    public class Name3
    {
        public string FullName { get; set; }
    }
}