using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<ReturnsSenseSettingsForAGivenCameraResponse>(myJsonResponse);
    public class ReturnsSenseSettingsForAGivenCameraResponse
    {
        public bool SenseEnabled { get; set; }
        public Authentication2 AudioDetection { get; set; }
        public string MqttBrokerId { get; set; }
        public List<string> MqttTopics { get; set; }
    }
}