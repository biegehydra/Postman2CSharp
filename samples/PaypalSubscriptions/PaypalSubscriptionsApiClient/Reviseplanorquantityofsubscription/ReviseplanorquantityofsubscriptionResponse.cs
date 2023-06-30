using System;
using System.Collections.Generic;
using System.Text.Json;

// Root myDeserializedClass = JsonSerializer.Deserialize<ReviseplanorquantityofsubscriptionResponse>(myJsonResponse);
namespace PaypalSubscriptions
{
    public class ReviseplanorquantityofsubscriptionResponse
    {
        public string PlanId { get; set; }
        public ShippingAmount ShippingAmount { get; set; }
        public ShippingAddress ShippingAddress { get; set; }
        public bool PlanOverridden { get; set; }
        public List<Links> Links { get; set; }
    }
}