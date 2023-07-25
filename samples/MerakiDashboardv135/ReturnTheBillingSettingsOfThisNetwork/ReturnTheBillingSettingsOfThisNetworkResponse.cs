using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<ReturnTheBillingSettingsOfThisNetworkResponse>(myJsonResponse);
    public class ReturnTheBillingSettingsOfThisNetworkResponse
    {
        public string Currency { get; set; }
        public List<Plans> Plans { get; set; }
    }

    public class Plans
    {
        public string Id { get; set; }
        public int Price { get; set; }
        public GlobalBandwidthLimits BandwidthLimits { get; set; }
        public string TimeLimit { get; set; }
    }
}