using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<List<AtedConnectivityInfoForThisNetworkGroupedByClientsResponse>>(myJsonResponse);
    public class AtedConnectivityInfoForThisNetworkGroupedByClientsResponse
    {
        public string Mac { get; set; }
        public ConnectionStats ConnectionStats { get; set; }
    }
}