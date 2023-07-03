using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateALinkAggregationGroupRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdateALinkAggregationGroupRequest
    {
        public List<SwitchPorts> SwitchPorts { get; set; }
        public List<SwitchProfilePorts> SwitchProfilePorts { get; set; }
    }
}