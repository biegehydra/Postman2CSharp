using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<EturnsQualityAndRetentionSettingsForTheGivenCameraResponse>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class EturnsQualityAndRetentionSettingsForTheGivenCameraResponse
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