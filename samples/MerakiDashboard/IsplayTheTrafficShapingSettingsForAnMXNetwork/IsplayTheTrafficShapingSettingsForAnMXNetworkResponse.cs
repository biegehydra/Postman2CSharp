using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<IsplayTheTrafficShapingSettingsForAnMXNetworkResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class IsplayTheTrafficShapingSettingsForAnMXNetworkResponse
    {
        public GlobalBandwidthLimits GlobalBandwidthLimits { get; set; }
    }

    public class GlobalBandwidthLimits
    {
        public int LimitUp { get; set; }
        public int LimitDown { get; set; }
    }
}