using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<SQualityAndRetentionSettingsForTheGivenCameraResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class SQualityAndRetentionSettingsForTheGivenCameraResponse
    {
        public bool MotionBasedRetentionEnabled { get; set; }
        public bool AudioRecordingEnabled { get; set; }
        public bool RestrictedBandwidthModeEnabled { get; set; }
        public string ProfileId { get; set; }
        public string Quality { get; set; }
        public int MotionDetectorVersion { get; set; }
        public string Resolution { get; set; }
    }
}