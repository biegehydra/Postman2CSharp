using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<DisplayTheTrafficShapingSettingsForAnMXNetworkResponse>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class DisplayTheTrafficShapingSettingsForAnMXNetworkResponse
    {
        public GlobalBandwidthLimits GlobalBandwidthLimits { get; set; }
    }

    public class GlobalBandwidthLimits
    {
        public int LimitUp { get; set; }
        public int LimitDown { get; set; }
    }
}