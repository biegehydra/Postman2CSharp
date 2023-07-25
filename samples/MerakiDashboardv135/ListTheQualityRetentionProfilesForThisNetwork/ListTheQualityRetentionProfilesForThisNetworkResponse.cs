using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<List<ListTheQualityRetentionProfilesForThisNetworkResponse>>(myJsonResponse);
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
        public MV12MV22MV72 MV32 { get; set; }
        public MV12MV22MV72 MV21MV71 { get; set; }
        public MV12MV22MV72 MV12MV22MV72 { get; set; }
        public MV12MV22MV72 MV12WE { get; set; }
    }

    public class MV12MV22MV72
    {
        public string Quality { get; set; }
        public string Resolution { get; set; }
    }
}