using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<CreateALinkAggregationGroupRequest>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class CreateALinkAggregationGroupRequest
    {
        public List<SwitchPorts> SwitchPorts { get; set; }
        public List<SwitchProfilePorts> SwitchProfilePorts { get; set; }
    }

    public class SwitchProfilePorts
    {
        public string Profile { get; set; }
        public string PortId { get; set; }
    }
}