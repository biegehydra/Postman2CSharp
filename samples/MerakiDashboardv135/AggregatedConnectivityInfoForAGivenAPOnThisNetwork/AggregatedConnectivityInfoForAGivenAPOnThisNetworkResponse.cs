using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<AggregatedConnectivityInfoForAGivenAPOnThisNetworkResponse>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class AggregatedConnectivityInfoForAGivenAPOnThisNetworkResponse
    {
        public string Serial { get; set; }
        public ConnectionStats ConnectionStats { get; set; }
    }

    public class ConnectionStats
    {
        public int Assoc { get; set; }
        public int Auth { get; set; }
        public int Dhcp { get; set; }
        public int Dns { get; set; }
        public int Success { get; set; }
    }
}