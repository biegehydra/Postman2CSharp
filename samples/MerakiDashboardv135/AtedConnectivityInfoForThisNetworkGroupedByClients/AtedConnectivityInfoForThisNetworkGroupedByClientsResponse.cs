using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<AtedConnectivityInfoForThisNetworkGroupedByClientsResponse>>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class AtedConnectivityInfoForThisNetworkGroupedByClientsResponse
    {
        public string Mac { get; set; }
        public ConnectionStats ConnectionStats { get; set; }
    }
}