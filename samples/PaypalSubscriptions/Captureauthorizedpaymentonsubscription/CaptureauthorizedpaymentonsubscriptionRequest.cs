using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PaypalSubscriptions
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<CaptureAuthorizedPaymentOnSubscriptionRequest>(myJsonResponse);
    public class CaptureAuthorizedPaymentOnSubscriptionRequest
    {
        [JsonPropertyName("note")]
        public string Note { get; set; }

        [JsonPropertyName("capture_type")]
        public string CaptureType { get; set; }

        [JsonPropertyName("amount")]
        public ShippingAmount Amount { get; set; }
    }
}