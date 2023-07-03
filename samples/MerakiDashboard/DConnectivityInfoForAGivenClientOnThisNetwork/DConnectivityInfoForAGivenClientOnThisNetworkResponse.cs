using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<DConnectivityInfoForAGivenClientOnThisNetworkResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class DConnectivityInfoForAGivenClientOnThisNetworkResponse
    {
        public string Mac { get; set; }
        public ConnectionStats ConnectionStats { get; set; }
    }
}