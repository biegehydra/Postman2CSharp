using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<List<EssLatencyOverTimeForANetworkDeviceOrNetworkClientResponse>>(myJsonResponse);
    public class EssLatencyOverTimeForANetworkDeviceOrNetworkClientResponse
    {
        public DateTime StartTs { get; set; }
        public DateTime EndTs { get; set; }
        public int AvgLatencyMs { get; set; }
    }
}