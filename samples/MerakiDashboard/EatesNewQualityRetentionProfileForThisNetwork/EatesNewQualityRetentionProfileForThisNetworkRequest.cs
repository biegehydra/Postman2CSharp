using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<EatesNewQualityRetentionProfileForThisNetworkRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class EatesNewQualityRetentionProfileForThisNetworkRequest
    {
        public string Name { get; set; }
        public string MotionBasedRetentionEnabled { get; set; }
        public string RestrictedBandwidthModeEnabled { get; set; }
        public string AudioRecordingEnabled { get; set; }
        public string CloudArchiveEnabled { get; set; }
        public string MotionDetectorVersion { get; set; }
        public string ScheduleId { get; set; }
        public string MaxRetentionDays { get; set; }
        public VideoSettings2 VideoSettings { get; set; }
    }

    public class VideoSettings2
    {
        [JsonPropertyName("MV21/MV71")]
        public MV32 MV21MV71 { get; set; }

        [JsonPropertyName("MV12/MV22/MV72")]
        public MV32 MV12MV22MV72 { get; set; }
        public MV32 MV32 { get; set; }
        public MV32 MV33 { get; set; }
        public MV32 MV12WE { get; set; }
        public MV32 MV13 { get; set; }

        [JsonPropertyName("MV22X/MV72X")]
        public MV32 MV22XMV72X { get; set; }
        public MV32 MV52 { get; set; }
        public MV32 MV63 { get; set; }
        public MV32 MV93 { get; set; }
        public MV32 MV63X { get; set; }
        public MV32 MV93X { get; set; }
    }
}