using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<ReturnsTheUplinkSettingsForYourMGNetworkResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class ReturnsTheUplinkSettingsForYourMGNetworkResponse
    {
        public GlobalBandwidthLimits BandwidthLimits { get; set; }
    }
}