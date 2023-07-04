using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdatesTheUplinkSettingsForYourMGNetworkResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdatesTheUplinkSettingsForYourMGNetworkResponse
    {
        public GlobalBandwidthLimits BandwidthLimits { get; set; }
    }
}