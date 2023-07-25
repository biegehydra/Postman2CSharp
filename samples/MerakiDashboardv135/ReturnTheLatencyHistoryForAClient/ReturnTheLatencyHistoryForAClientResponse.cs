using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<List<ReturnTheLatencyHistoryForAClientResponse>>(myJsonResponse);
    public class ReturnTheLatencyHistoryForAClientResponse
    {
        public int T0 { get; set; }
        public int T1 { get; set; }
        public LatencyBinsByCategory LatencyBinsByCategory { get; set; }
    }

    public class BackgroundTraffic2
    {
        public int _05 { get; set; }
        public int _10 { get; set; }
        public int _20 { get; set; }
        public int _40 { get; set; }
        public int _80 { get; set; }
        public int _160 { get; set; }
        public int _320 { get; set; }
        public int _640 { get; set; }
        public int _1280 { get; set; }
        public int _2560 { get; set; }
        public int _5120 { get; set; }
        public int _10240 { get; set; }
        public int _20480 { get; set; }
    }

    public class LatencyBinsByCategory
    {
        public BackgroundTraffic2 BackgroundTraffic { get; set; }
        public BackgroundTraffic BestEffortTraffic { get; set; }
        public BackgroundTraffic VideoTraffic { get; set; }
        public BackgroundTraffic VoiceTraffic { get; set; }
    }
}