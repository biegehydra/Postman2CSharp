using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<DisplayTheTrafficShapingSettingsForAnMXNetworkResponse>(myJsonResponse);
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