using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<CycleASetOfSwitchPortsRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class CycleASetOfSwitchPortsRequest
    {
        public List<string> Ports { get; set; }
    }
}