using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<EturnTheStormControlConfigurationForASwitchNetworkResponse>(myJsonResponse);
    public class EturnTheStormControlConfigurationForASwitchNetworkResponse
    {
        public int BroadcastThreshold { get; set; }
        public int MulticastThreshold { get; set; }
        public int UnknownUnicastThreshold { get; set; }
    }
}