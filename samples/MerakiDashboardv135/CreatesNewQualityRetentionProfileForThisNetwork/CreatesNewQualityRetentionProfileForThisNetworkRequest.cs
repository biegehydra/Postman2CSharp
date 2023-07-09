using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<CreatesNewQualityRetentionProfileForThisNetworkRequest>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class CreatesNewQualityRetentionProfileForThisNetworkRequest
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
        public MV21MV71 MV21MV71 { get; set; }

        [JsonPropertyName("MV12/MV22/MV72")]
        public MV12MV22MV72 MV12MV22MV72 { get; set; }
        public MV32 MV32 { get; set; }
        public MV33 MV33 { get; set; }
        public MV12WE MV12WE { get; set; }
        public MV13 MV13 { get; set; }

        [JsonPropertyName("MV22X/MV72X")]
        public MV22XMV72X MV22XMV72X { get; set; }
        public MV52 MV52 { get; set; }
        public MV63 MV63 { get; set; }
        public MV93 MV93 { get; set; }
        public MV63X MV63X { get; set; }
        public MV93X MV93X { get; set; }
    }

    public class MV13
    {
        public string Quality { get; set; }
        public string Resolution { get; set; }
    }

    public class MV22XMV72X
    {
        public string Quality { get; set; }
        public string Resolution { get; set; }
    }

    public class MV33
    {
        public string Quality { get; set; }
        public string Resolution { get; set; }
    }

    public class MV52
    {
        public string Quality { get; set; }
        public string Resolution { get; set; }
    }

    public class MV63
    {
        public string Quality { get; set; }
        public string Resolution { get; set; }
    }

    public class MV63X
    {
        public string Quality { get; set; }
        public string Resolution { get; set; }
    }

    public class MV93
    {
        public string Quality { get; set; }
        public string Resolution { get; set; }
    }

    public class MV93X
    {
        public string Quality { get; set; }
        public string Resolution { get; set; }
    }
}