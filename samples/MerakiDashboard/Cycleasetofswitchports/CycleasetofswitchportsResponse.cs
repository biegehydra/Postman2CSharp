using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<CycleASetOfSwitchPortsResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class CycleASetOfSwitchPortsResponse
    {
        public List<string> Ports { get; set; }
    }
}