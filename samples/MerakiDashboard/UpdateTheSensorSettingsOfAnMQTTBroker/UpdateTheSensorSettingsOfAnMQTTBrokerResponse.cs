using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateTheSensorSettingsOfAnMQTTBrokerResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdateTheSensorSettingsOfAnMQTTBrokerResponse
    {
        public string MqttBrokerId { get; set; }
        public bool Enabled { get; set; }
    }
}