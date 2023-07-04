using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<ReturnsSenseSettingsForAGivenCameraResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class ReturnsSenseSettingsForAGivenCameraResponse
    {
        public bool SenseEnabled { get; set; }
        public Wan2 AudioDetection { get; set; }
        public string MqttBrokerId { get; set; }
        public List<string> MqttTopics { get; set; }
    }
}