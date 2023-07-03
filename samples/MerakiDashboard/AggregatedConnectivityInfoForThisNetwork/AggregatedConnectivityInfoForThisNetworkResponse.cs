using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<AggregatedConnectivityInfoForThisNetworkResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class AggregatedConnectivityInfoForThisNetworkResponse
    {
        public int Assoc { get; set; }
        public int Auth { get; set; }
        public int Dhcp { get; set; }
        public int Dns { get; set; }
        public int Success { get; set; }
    }
}