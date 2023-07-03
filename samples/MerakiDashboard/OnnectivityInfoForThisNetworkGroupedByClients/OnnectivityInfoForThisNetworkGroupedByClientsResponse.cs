using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<OnnectivityInfoForThisNetworkGroupedByClientsResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class OnnectivityInfoForThisNetworkGroupedByClientsResponse
    {
        public string Mac { get; set; }
        public ConnectionStats ConnectionStats { get; set; }
    }
}