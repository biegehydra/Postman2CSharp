using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PaypalSubscriptions
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<CreateSubscriptionRequest>(myJsonResponse);
    public class CreateSubscriptionRequest
    {
        [JsonPropertyName("plan_id")]
        public string PlanId { get; set; }

        [JsonPropertyName("start_time")]
        public string StartTime { get; set; }

        [JsonPropertyName("shipping_amount")]
        public ShippingAmount ShippingAmount { get; set; }

        [JsonPropertyName("subscriber")]
        public Subscriber Subscriber { get; set; }

        [JsonPropertyName("application_context")]
        public ApplicationContext ApplicationContext { get; set; }
    }

    public class ApplicationContext
    {
        [JsonPropertyName("brand_name")]
        public string BrandName { get; set; }

        [JsonPropertyName("locale")]
        public string Locale { get; set; }

        [JsonPropertyName("shipping_preference")]
        public string ShippingPreference { get; set; }

        [JsonPropertyName("user_action")]
        public string UserAction { get; set; }

        [JsonPropertyName("payment_method")]
        public PaymentMethod PaymentMethod { get; set; }

        [JsonPropertyName("return_url")]
        public string ReturnUrl { get; set; }

        [JsonPropertyName("cancel_url")]
        public string CancelUrl { get; set; }
    }

    public class Address
    {
        [JsonPropertyName("address_line_1")]
        public string AddressLine1 { get; set; }

        [JsonPropertyName("address_line_2")]
        public string AddressLine2 { get; set; }

        [JsonPropertyName("admin_area_2")]
        public string AdminArea2 { get; set; }

        [JsonPropertyName("admin_area_1")]
        public string AdminArea1 { get; set; }

        [JsonPropertyName("postal_code")]
        public string PostalCode { get; set; }

        [JsonPropertyName("country_code")]
        public string CountryCode { get; set; }
    }

    public class Subscriber
    {
        [JsonPropertyName("name")]
        public Name Name { get; set; }

        [JsonPropertyName("email_address")]
        public string EmailAddress { get; set; }

        [JsonPropertyName("shipping_address")]
        public ShippingAddress ShippingAddress { get; set; }
    }

    public class Name
    {
        [JsonPropertyName("given_name")]
        public string GivenName { get; set; }

        [JsonPropertyName("surname")]
        public string Surname { get; set; }

        [JsonPropertyName("full_name")]
        public string FullName { get; set; }
    }

    public class PaymentMethod
    {
        [JsonPropertyName("payer_selected")]
        public string PayerSelected { get; set; }

        [JsonPropertyName("payee_preferred")]
        public string PayeePreferred { get; set; }
    }

    public class ShippingAddress
    {
        [JsonPropertyName("name")]
        public Name Name { get; set; }

        [JsonPropertyName("address")]
        public Address Address { get; set; }
    }

    public class ShippingAmount
    {
        [JsonPropertyName("currency_code")]
        public string CurrencyCode { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}