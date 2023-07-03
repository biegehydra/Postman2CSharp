using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<TheStormControlConfigurationForASwitchNetwork2Response>(myJsonResponse);
namespace MerakiDashboard
{
    public class TheStormControlConfigurationForASwitchNetwork2Response
    {
        public int BroadcastThreshold { get; set; }
        public int MulticastThreshold { get; set; }
        public int UnknownUnicastThreshold { get; set; }
    }
}