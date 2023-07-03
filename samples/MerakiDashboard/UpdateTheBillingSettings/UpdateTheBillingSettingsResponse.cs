using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateTheBillingSettingsResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdateTheBillingSettingsResponse
    {
        public string Currency { get; set; }
        public List<Plans> Plans { get; set; }
    }
}