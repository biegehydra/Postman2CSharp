using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<EQualityAndRetentionSettingsForTheGivenCameraRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class EQualityAndRetentionSettingsForTheGivenCameraRequest
    {
        public string ProfileId { get; set; }
        public string MotionBasedRetentionEnabled { get; set; }
        public string AudioRecordingEnabled { get; set; }
        public string RestrictedBandwidthModeEnabled { get; set; }
        public string Quality { get; set; }
        public string Resolution { get; set; }
        public string MotionDetectorVersion { get; set; }
    }
}