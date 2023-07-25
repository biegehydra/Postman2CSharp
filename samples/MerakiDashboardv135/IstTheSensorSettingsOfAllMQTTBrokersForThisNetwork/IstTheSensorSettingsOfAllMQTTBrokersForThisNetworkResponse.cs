using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<List<IstTheSensorSettingsOfAllMQTTBrokersForThisNetworkResponse>>(myJsonResponse);
    public class IstTheSensorSettingsOfAllMQTTBrokersForThisNetworkResponse
    {
        public string MqttBrokerId { get; set; }
        public bool Enabled { get; set; }
    }
}