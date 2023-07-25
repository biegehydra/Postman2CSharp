using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<AggregatedLatencyInfoForAGivenAPOnThisNetworkResponse>(myJsonResponse);
    public class AggregatedLatencyInfoForAGivenAPOnThisNetworkResponse
    {
        public string Serial { get; set; }
        public LatencyStats LatencyStats { get; set; }
    }

    public class RawDistribution
    {
        public int _0 { get; set; }
        public int _1 { get; set; }
        public int _2 { get; set; }
        public int _4 { get; set; }
        public int _8 { get; set; }
        public int _16 { get; set; }
        public int _32 { get; set; }
        public int _64 { get; set; }
        public int _128 { get; set; }
        public int _256 { get; set; }
        public int _512 { get; set; }
        public int _1024 { get; set; }
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