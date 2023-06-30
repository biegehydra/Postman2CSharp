using System;
using System.Collections.Generic;
using System.Text.Json;

// Root myDeserializedClass = JsonSerializer.Deserialize<ReviseplanorquantityofsubscriptionRequest>(myJsonResponse);
namespace PaypalSubscriptions
{
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

    public class ReviseplanorquantityofsubscriptionRequest
    {
        public string PlanId { get; set; }
        public ShippingAmount ShippingAmount { get; set; }
        public ShippingAddress ShippingAddress { get; set; }
        public ApplicationContext2 ApplicationContext { get; set; }
    }
}