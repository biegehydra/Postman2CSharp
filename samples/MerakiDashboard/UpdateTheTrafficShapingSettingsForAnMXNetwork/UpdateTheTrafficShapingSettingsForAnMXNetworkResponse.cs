using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateTheTrafficShapingSettingsForAnMXNetworkResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdateTheTrafficShapingSettingsForAnMXNetworkResponse
    {
        public GlobalBandwidthLimits GlobalBandwidthLimits { get; set; }
    }
}