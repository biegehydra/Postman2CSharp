using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<CreatesNewQualityRetentionProfileForThisNetworkRequest>(myJsonResponse);
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
        public MV12MV22MV72 MV21MV71 { get; set; }
        public MV12MV22MV72 MV12MV22MV72 { get; set; }
        public MV12MV22MV72 MV32 { get; set; }
        public MV12MV22MV72 MV33 { get; set; }
        public MV12MV22MV72 MV12WE { get; set; }
        public MV12MV22MV72 MV13 { get; set; }
        public MV12MV22MV72 MV22XMV72X { get; set; }
        public MV12MV22MV72 MV52 { get; set; }
        public MV12MV22MV72 MV63 { get; set; }
        public MV12MV22MV72 MV93 { get; set; }
        public MV12MV22MV72 MV63X { get; set; }
        public MV12MV22MV72 MV93X { get; set; }
    }
}