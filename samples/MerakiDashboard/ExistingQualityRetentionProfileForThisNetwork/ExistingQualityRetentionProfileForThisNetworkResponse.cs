using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<ExistingQualityRetentionProfileForThisNetworkResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class ExistingQualityRetentionProfileForThisNetworkResponse
    {
        public string Id { get; set; }
        public string NetworkId { get; set; }
        public string Name { get; set; }
        public bool RestrictedBandwidthModeEnabled { get; set; }
        public bool MotionBasedRetentionEnabled { get; set; }
        public bool AudioRecordingEnabled { get; set; }
        public bool CloudArchiveEnabled { get; set; }
        public int MaxRetentionDays { get; set; }
        public object ScheduleId { get; set; }
        public int MotionDetectorVersion { get; set; }
        public VideoSettings VideoSettings { get; set; }
    }
}