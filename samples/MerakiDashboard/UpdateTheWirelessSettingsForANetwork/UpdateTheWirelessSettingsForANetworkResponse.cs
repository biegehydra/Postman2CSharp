using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateTheWirelessSettingsForANetworkResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdateTheWirelessSettingsForANetworkResponse
    {
        public bool MeshingEnabled { get; set; }
        public bool Ipv6BridgeEnabled { get; set; }
        public bool LocationAnalyticsEnabled { get; set; }
        public string UpgradeStrategy { get; set; }
        public bool LedLightsOn { get; set; }
        public NamedVlans NamedVlans { get; set; }
    }
}