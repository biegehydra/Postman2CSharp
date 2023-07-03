using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdatesSTPSettingsRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdatesSTPSettingsRequest
    {
        public string RstpEnabled { get; set; }
        public List<StpBridgePriority2> StpBridgePriority { get; set; }
    }

    public class StpBridgePriority2
    {
        public string StpPriority { get; set; }
        public List<string> SwitchProfiles { get; set; }
        public List<string> Switches { get; set; }
        public List<string> Stacks { get; set; }
    }
}