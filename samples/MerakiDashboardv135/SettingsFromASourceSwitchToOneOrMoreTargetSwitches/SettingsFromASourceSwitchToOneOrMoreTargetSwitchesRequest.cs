using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<SettingsFromASourceSwitchToOneOrMoreTargetSwitchesRequest>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class SettingsFromASourceSwitchToOneOrMoreTargetSwitchesRequest
    {
        public string SourceSerial { get; set; }
        public List<string> TargetSerials { get; set; }
    }
}