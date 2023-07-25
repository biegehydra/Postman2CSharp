using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<ReturnsTheUplinkSettingsForYourMGNetworkResponse>(myJsonResponse);
    public class ReturnsTheUplinkSettingsForYourMGNetworkResponse
    {
        public GlobalBandwidthLimits BandwidthLimits { get; set; }
    }
}