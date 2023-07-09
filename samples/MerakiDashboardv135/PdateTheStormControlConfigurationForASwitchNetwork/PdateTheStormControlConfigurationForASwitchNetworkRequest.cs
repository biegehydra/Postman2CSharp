using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<PdateTheStormControlConfigurationForASwitchNetworkRequest>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class PdateTheStormControlConfigurationForASwitchNetworkRequest
    {
        public string BroadcastThreshold { get; set; }
        public string MulticastThreshold { get; set; }
        public string UnknownUnicastThreshold { get; set; }
    }
}