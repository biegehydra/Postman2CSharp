using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<UpdateSenseSettingsForTheGivenCameraRequest>(myJsonResponse);
    public class UpdateSenseSettingsForTheGivenCameraRequest
    {
        public string SenseEnabled { get; set; }
        public string MqttBrokerId { get; set; }
        public Dot11w AudioDetection { get; set; }
        public string DetectionModelId { get; set; }
    }
}