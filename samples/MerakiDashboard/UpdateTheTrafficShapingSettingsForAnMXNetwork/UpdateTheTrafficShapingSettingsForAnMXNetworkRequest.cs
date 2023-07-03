using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateTheTrafficShapingSettingsForAnMXNetworkRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdateTheTrafficShapingSettingsForAnMXNetworkRequest
    {
        public GlobalBandwidthLimits2 GlobalBandwidthLimits { get; set; }
    }

    public class GlobalBandwidthLimits2
    {
        public string LimitUp { get; set; }
        public string LimitDown { get; set; }
    }
}