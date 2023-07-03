using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<ListTheQualityRetentionProfilesForThisNetworkResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class ListTheQualityRetentionProfilesForThisNetworkResponse
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

    public class VideoSettings
    {
        public MV32 MV32 { get; set; }

        [JsonPropertyName("MV21/MV71")]
        public MV21MV71 MV21MV71 { get; set; }

        [JsonPropertyName("MV12/MV22/MV72")]
        public MV12MV22MV72 MV12MV22MV72 { get; set; }
        public MV12WE MV12WE { get; set; }
    }

    public class MV12MV22MV72
    {
        public string Quality { get; set; }
        public string Resolution { get; set; }
    }

    public class MV12WE
    {
        public string Quality { get; set; }
        public string Resolution { get; set; }
    }

    public class MV21MV71
    {
        public string Quality { get; set; }
        public string Resolution { get; set; }
    }

    public class MV32
    {
        public string Quality { get; set; }
        public string Resolution { get; set; }
    }
}