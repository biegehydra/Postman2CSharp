using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<CreateALinkAggregationGroupRequest>(myJsonResponse);
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