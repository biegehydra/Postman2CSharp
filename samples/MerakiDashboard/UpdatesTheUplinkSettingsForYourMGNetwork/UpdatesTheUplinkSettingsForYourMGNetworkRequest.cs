using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdatesTheUplinkSettingsForYourMGNetworkRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdatesTheUplinkSettingsForYourMGNetworkRequest
    {
        public BandwidthLimits2 BandwidthLimits { get; set; }
    }

    public class BandwidthLimits2
    {
        public string LimitUp { get; set; }
        public string LimitDown { get; set; }
    }
}