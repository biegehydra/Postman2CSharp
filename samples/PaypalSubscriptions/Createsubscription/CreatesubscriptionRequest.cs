using System;
using System.Collections.Generic;
using System.Text.Json;

// Root myDeserializedClass = JsonSerializer.Deserialize<CreatesubscriptionRequest>(myJsonResponse);
namespace PaypalSubscriptions
{
    public class Address
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AdminArea2 { get; set; }
        public string AdminArea1 { get; set; }
        public string PostalCode { get; set; }
        public string CountryCode { get; set; }
    }

    public class ApplicationContext
    {
        public string BrandName { get; set; }
        public string Locale { get; set; }
        public string ShippingPreference { get; set; }
        public string UserAction { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string ReturnUrl { get; set; }
        public string CancelUrl { get; set; }
    }

    public class CreatesubscriptionRequest
    {
        public string PlanId { get; set; }
        public string StartTime { get; set; }
        public ShippingAmount ShippingAmount { get; set; }
        public Subscriber Subscriber { get; set; }
        public ApplicationContext ApplicationContext { get; set; }
    }

    public class Name
    {
        public string GivenName { get; set; }
        public string Surname { get; set; }
        public string FullName { get; set; }
    }

    public class PaymentMethod
    {
        public string PayerSelected { get; set; }
        public string PayeePreferred { get; set; }
    }

    public class ShippingAddress
    {
        public Name2 Name { get; set; }
        public Address Address { get; set; }
    }

    public class ShippingAmount
    {
        public string CurrencyCode { get; set; }
        public string Value { get; set; }
    }

    public class Subscriber
    {
        public Name Name { get; set; }
        public string EmailAddress { get; set; }
        public ShippingAddress ShippingAddress { get; set; }
    }
}