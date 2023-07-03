using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<AggregatedLatencyInfoForAGivenAPOnThisNetworkResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class AggregatedLatencyInfoForAGivenAPOnThisNetworkResponse
    {
        public string Serial { get; set; }
        public LatencyStats LatencyStats { get; set; }
    }

    public class RawDistribution
    {
        [JsonPropertyName("0")]
        public int _0 { get; set; }

        [JsonPropertyName("1")]
        public int _1 { get; set; }

        [JsonPropertyName("2")]
        public int _2 { get; set; }

        [JsonPropertyName("4")]
        public int _4 { get; set; }

        [JsonPropertyName("8")]
        public int _8 { get; set; }

        [JsonPropertyName("16")]
        public int _16 { get; set; }

        [JsonPropertyName("32")]
        public int _32 { get; set; }

        [JsonPropertyName("64")]
        public int _64 { get; set; }

        [JsonPropertyName("128")]
        public int _128 { get; set; }

        [JsonPropertyName("256")]
        public int _256 { get; set; }

        [JsonPropertyName("512")]
        public int _512 { get; set; }

        [JsonPropertyName("1024")]
        public int _1024 { get; set; }

        [JsonPropertyName("2048")]
        public int _2048 { get; set; }
    }

    public class LatencyStats
    {
        public BackgroundTraffic BackgroundTraffic { get; set; }
        public string BestEffortTraffic { get; set; }
        public string VideoTraffic { get; set; }
        public string VoiceTraffic { get; set; }
    }

    public class BackgroundTraffic
    {
        public RawDistribution RawDistribution { get; set; }
        public double Avg { get; set; }
    }
}