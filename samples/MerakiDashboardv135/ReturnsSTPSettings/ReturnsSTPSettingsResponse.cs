using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<ReturnsSTPSettingsResponse>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class ReturnsSTPSettingsResponse
    {
        public bool RstpEnabled { get; set; }
        public List<StpBridgePriority> StpBridgePriority { get; set; }
    }

    public class StpBridgePriority
    {
        public List<string> Switches { get; set; }
        public int StpPriority { get; set; }
        public List<string> Stacks { get; set; }
    }
}