using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<List<GgregatedLatencyInfoForThisNetworkGroupedByClientsResponse>>(myJsonResponse);
    public class GgregatedLatencyInfoForThisNetworkGroupedByClientsResponse
    {
        public string Mac { get; set; }
        public LatencyStats LatencyStats { get; set; }
    }
}