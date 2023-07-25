using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<SettingsFromASourceSwitchToOneOrMoreTargetSwitchesRequest>(myJsonResponse);
    public class SettingsFromASourceSwitchToOneOrMoreTargetSwitchesRequest
    {
        public string SourceSerial { get; set; }
        public List<string> TargetSerials { get; set; }
    }
}