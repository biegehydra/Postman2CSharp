using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<List<NdGoodputInKilobitsPerSecondForAWiredNetworkDeviceResponse>>(myJsonResponse);
    public class NdGoodputInKilobitsPerSecondForAWiredNetworkDeviceResponse
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double LossPercent { get; set; }
        public double LatencyMs { get; set; }
        public int Goodput { get; set; }
        public double Jitter { get; set; }
    }
}