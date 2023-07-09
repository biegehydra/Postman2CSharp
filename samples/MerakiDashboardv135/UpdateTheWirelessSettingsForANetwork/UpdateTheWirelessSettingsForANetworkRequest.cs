using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateTheWirelessSettingsForANetworkRequest>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class UpdateTheWirelessSettingsForANetworkRequest
    {
        public string MeshingEnabled { get; set; }
        public string Ipv6BridgeEnabled { get; set; }
        public string LocationAnalyticsEnabled { get; set; }
        public string UpgradeStrategy { get; set; }
        public string LedLightsOn { get; set; }
    }
}