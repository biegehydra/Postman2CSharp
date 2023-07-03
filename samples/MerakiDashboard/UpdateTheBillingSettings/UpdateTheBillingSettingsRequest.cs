using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateTheBillingSettingsRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdateTheBillingSettingsRequest
    {
        public string Currency { get; set; }
        public List<Plans2> Plans { get; set; }
    }

    public class Plans2
    {
        public string Price { get; set; }
        public BandwidthLimits BandwidthLimits { get; set; }
        public string TimeLimit { get; set; }
        public string Id { get; set; }
    }
}