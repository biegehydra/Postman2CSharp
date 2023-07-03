using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateTheSensorSettingsOfAnMQTTBrokerRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdateTheSensorSettingsOfAnMQTTBrokerRequest
    {
        public string Enabled { get; set; }
    }
}