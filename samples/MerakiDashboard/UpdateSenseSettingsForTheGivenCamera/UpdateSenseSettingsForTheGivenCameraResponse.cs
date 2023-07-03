using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateSenseSettingsForTheGivenCameraResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdateSenseSettingsForTheGivenCameraResponse
    {
        public bool SenseEnabled { get; set; }
        public AudioDetection AudioDetection { get; set; }
        public string MqttBrokerId { get; set; }
        public List<string> MqttTopics { get; set; }
    }
}