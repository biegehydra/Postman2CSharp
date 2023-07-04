using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdatesTheUplinkSettingsForYourMGNetworkRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdatesTheUplinkSettingsForYourMGNetworkRequest
    {
        public GlobalBandwidthLimits2 BandwidthLimits { get; set; }
    }
}