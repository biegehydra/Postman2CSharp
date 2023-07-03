using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateSenseSettingsForTheGivenCameraRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdateSenseSettingsForTheGivenCameraRequest
    {
        public string SenseEnabled { get; set; }
        public string MqttBrokerId { get; set; }
        public AudioDetection2 AudioDetection { get; set; }
        public string DetectionModelId { get; set; }
    }

    public class AudioDetection2
    {
        public string Enabled { get; set; }
    }
}