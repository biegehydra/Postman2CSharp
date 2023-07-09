using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<EturnTheStormControlConfigurationForASwitchNetworkResponse>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class EturnTheStormControlConfigurationForASwitchNetworkResponse
    {
        public int BroadcastThreshold { get; set; }
        public int MulticastThreshold { get; set; }
        public int UnknownUnicastThreshold { get; set; }
    }
}