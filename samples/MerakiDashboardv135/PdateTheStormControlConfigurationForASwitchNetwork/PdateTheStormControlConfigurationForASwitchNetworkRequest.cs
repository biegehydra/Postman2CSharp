using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<PdateTheStormControlConfigurationForASwitchNetworkRequest>(myJsonResponse);
    public class PdateTheStormControlConfigurationForASwitchNetworkRequest
    {
        public string BroadcastThreshold { get; set; }
        public string MulticastThreshold { get; set; }
        public string UnknownUnicastThreshold { get; set; }
    }
}