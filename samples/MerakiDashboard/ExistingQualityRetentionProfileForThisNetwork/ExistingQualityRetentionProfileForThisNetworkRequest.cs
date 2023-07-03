using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<ExistingQualityRetentionProfileForThisNetworkRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class ExistingQualityRetentionProfileForThisNetworkRequest
    {
        public string Name { get; set; }
        public string MotionBasedRetentionEnabled { get; set; }
        public string RestrictedBandwidthModeEnabled { get; set; }
        public string AudioRecordingEnabled { get; set; }
        public string CloudArchiveEnabled { get; set; }
        public string MotionDetectorVersion { get; set; }
        public string ScheduleId { get; set; }
        public string MaxRetentionDays { get; set; }
        public VideoSettings VideoSettings { get; set; }
    }
}