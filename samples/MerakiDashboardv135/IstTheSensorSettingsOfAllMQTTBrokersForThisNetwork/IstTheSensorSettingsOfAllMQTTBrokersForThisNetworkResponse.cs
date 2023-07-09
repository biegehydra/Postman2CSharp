using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<IstTheSensorSettingsOfAllMQTTBrokersForThisNetworkResponse>>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class IstTheSensorSettingsOfAllMQTTBrokersForThisNetworkResponse
    {
        public string MqttBrokerId { get; set; }
        public bool Enabled { get; set; }
    }
}