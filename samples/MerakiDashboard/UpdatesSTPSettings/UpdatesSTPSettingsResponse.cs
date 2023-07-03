using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdatesSTPSettingsResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdatesSTPSettingsResponse
    {
        public bool RstpEnabled { get; set; }
        public List<StpBridgePriority> StpBridgePriority { get; set; }
    }
}