using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<AggregatedLatencyInfoForThisNetworkResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class AggregatedLatencyInfoForThisNetworkResponse
    {
        public BackgroundTraffic BackgroundTraffic { get; set; }
        public string BestEffortTraffic { get; set; }
        public string VideoTraffic { get; set; }
        public string VoiceTraffic { get; set; }
    }
}