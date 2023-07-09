using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<GgregatedLatencyInfoForThisNetworkGroupedByClientsResponse>>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class GgregatedLatencyInfoForThisNetworkGroupedByClientsResponse
    {
        public string Mac { get; set; }
        public LatencyStats LatencyStats { get; set; }
    }
}