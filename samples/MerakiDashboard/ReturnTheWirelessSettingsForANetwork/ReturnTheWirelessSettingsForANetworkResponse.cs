using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<ReturnTheWirelessSettingsForANetworkResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class ReturnTheWirelessSettingsForANetworkResponse
    {
        public bool MeshingEnabled { get; set; }
        public bool Ipv6BridgeEnabled { get; set; }
        public bool LocationAnalyticsEnabled { get; set; }
        public string UpgradeStrategy { get; set; }
        public bool LedLightsOn { get; set; }
        public NamedVlans NamedVlans { get; set; }
    }

    public class PoolDhcpMonitoring
    {
        public bool Enabled { get; set; }
        public int Duration { get; set; }
    }

    public class NamedVlans
    {
        public PoolDhcpMonitoring PoolDhcpMonitoring { get; set; }
    }
}