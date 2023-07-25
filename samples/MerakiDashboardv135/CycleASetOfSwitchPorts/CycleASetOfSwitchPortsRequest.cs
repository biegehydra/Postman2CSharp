using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<CycleASetOfSwitchPortsRequest>(myJsonResponse);
    public class CycleASetOfSwitchPortsRequest
    {
        public List<string> Ports { get; set; }
    }
}