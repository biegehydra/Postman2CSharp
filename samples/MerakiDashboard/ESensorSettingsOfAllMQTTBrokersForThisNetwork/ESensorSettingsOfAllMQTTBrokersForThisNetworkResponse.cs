using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<ESensorSettingsOfAllMQTTBrokersForThisNetworkResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class ESensorSettingsOfAllMQTTBrokersForThisNetworkResponse
    {
        public string MqttBrokerId { get; set; }
        public bool Enabled { get; set; }
    }
}