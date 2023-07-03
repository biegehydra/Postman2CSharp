using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<AtedLatencyInfoForThisNetworkGroupedByClientsResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class AtedLatencyInfoForThisNetworkGroupedByClientsResponse
    {
        public string Mac { get; set; }
        public LatencyStats LatencyStats { get; set; }
    }
}