using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<ReturnsTheUplinkSettingsForYourMGNetworkResponse>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class ReturnsTheUplinkSettingsForYourMGNetworkResponse
    {
        public BandwidthLimits BandwidthLimits { get; set; }
    }

    public class BandwidthLimits
    {
        public int LimitUp { get; set; }
        public int LimitDown { get; set; }
    }
}