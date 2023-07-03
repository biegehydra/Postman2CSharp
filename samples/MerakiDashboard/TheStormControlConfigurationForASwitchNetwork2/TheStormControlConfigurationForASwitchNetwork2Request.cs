using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<TheStormControlConfigurationForASwitchNetwork2Request>(myJsonResponse);
namespace MerakiDashboard
{
    public class TheStormControlConfigurationForASwitchNetwork2Request
    {
        public string BroadcastThreshold { get; set; }
        public string MulticastThreshold { get; set; }
        public string UnknownUnicastThreshold { get; set; }
    }
}