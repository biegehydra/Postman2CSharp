using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<ReturnthelatencyhistoryforaclientResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class ReturnthelatencyhistoryforaclientResponse
    {
        public int T0 { get; set; }
        public int T1 { get; set; }
        public LatencyBinsByCategory LatencyBinsByCategory { get; set; }
    }

    public class BackgroundTraffic5
    {
        [JsonPropertyName("0.5")]
        public int _05 { get; set; }

        [JsonPropertyName("1.0")]
        public int _10 { get; set; }

        [JsonPropertyName("2.0")]
        public int _20 { get; set; }

        [JsonPropertyName("4.0")]
        public int _40 { get; set; }

        [JsonPropertyName("8.0")]
        public int _80 { get; set; }

        [JsonPropertyName("16.0")]
        public int _160 { get; set; }

        [JsonPropertyName("32.0")]
        public int _320 { get; set; }

        [JsonPropertyName("64.0")]
        public int _640 { get; set; }

        [JsonPropertyName("128.0")]
        public int _1280 { get; set; }

        [JsonPropertyName("256.0")]
        public int _2560 { get; set; }

        [JsonPropertyName("512.0")]
        public int _5120 { get; set; }

        [JsonPropertyName("1024.0")]
        public int _10240 { get; set; }

        [JsonPropertyName("2048.0")]
        public int _20480 { get; set; }
    }

    public class BestEffortTraffic
    {
        [JsonPropertyName("0.5")]
        public int _05 { get; set; }

        [JsonPropertyName("1.0")]
        public int _10 { get; set; }

        [JsonPropertyName("2.0")]
        public int _20 { get; set; }

        [JsonPropertyName("4.0")]
        public int _40 { get; set; }

        [JsonPropertyName("8.0")]
        public int _80 { get; set; }

        [JsonPropertyName("16.0")]
        public int _160 { get; set; }

        [JsonPropertyName("32.0")]
        public int _320 { get; set; }

        [JsonPropertyName("64.0")]
        public int _640 { get; set; }

        [JsonPropertyName("128.0")]
        public int _1280 { get; set; }

        [JsonPropertyName("256.0")]
        public int _2560 { get; set; }

        [JsonPropertyName("512.0")]
        public int _5120 { get; set; }

        [JsonPropertyName("1024.0")]
        public int _10240 { get; set; }

        [JsonPropertyName("2048.0")]
        public int _20480 { get; set; }
    }

    public class VideoTraffic
    {
        [JsonPropertyName("0.5")]
        public int _05 { get; set; }

        [JsonPropertyName("1.0")]
        public int _10 { get; set; }

        [JsonPropertyName("2.0")]
        public int _20 { get; set; }

        [JsonPropertyName("4.0")]
        public int _40 { get; set; }

        [JsonPropertyName("8.0")]
        public int _80 { get; set; }

        [JsonPropertyName("16.0")]
        public int _160 { get; set; }

        [JsonPropertyName("32.0")]
        public int _320 { get; set; }

        [JsonPropertyName("64.0")]
        public int _640 { get; set; }

        [JsonPropertyName("128.0")]
        public int _1280 { get; set; }

        [JsonPropertyName("256.0")]
        public int _2560 { get; set; }

        [JsonPropertyName("512.0")]
        public int _5120 { get; set; }

        [JsonPropertyName("1024.0")]
        public int _10240 { get; set; }

        [JsonPropertyName("2048.0")]
        public int _20480 { get; set; }
    }

    public class VoiceTraffic
    {
        [JsonPropertyName("0.5")]
        public int _05 { get; set; }

        [JsonPropertyName("1.0")]
        public int _10 { get; set; }

        [JsonPropertyName("2.0")]
        public int _20 { get; set; }

        [JsonPropertyName("4.0")]
        public int _40 { get; set; }

        [JsonPropertyName("8.0")]
        public int _80 { get; set; }

        [JsonPropertyName("16.0")]
        public int _160 { get; set; }

        [JsonPropertyName("32.0")]
        public int _320 { get; set; }

        [JsonPropertyName("64.0")]
        public int _640 { get; set; }

        [JsonPropertyName("128.0")]
        public int _1280 { get; set; }

        [JsonPropertyName("256.0")]
        public int _2560 { get; set; }

        [JsonPropertyName("512.0")]
        public int _5120 { get; set; }

        [JsonPropertyName("1024.0")]
        public int _10240 { get; set; }

        [JsonPropertyName("2048.0")]
        public int _20480 { get; set; }
    }

    public class LatencyBinsByCategory
    {
        public BackgroundTraffic5 BackgroundTraffic { get; set; }
        public BestEffortTraffic BestEffortTraffic { get; set; }
        public VideoTraffic VideoTraffic { get; set; }
        public VoiceTraffic VoiceTraffic { get; set; }
    }
}