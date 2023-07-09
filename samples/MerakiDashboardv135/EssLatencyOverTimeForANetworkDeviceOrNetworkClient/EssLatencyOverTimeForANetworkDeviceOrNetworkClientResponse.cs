using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<EssLatencyOverTimeForANetworkDeviceOrNetworkClientResponse>>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class EssLatencyOverTimeForANetworkDeviceOrNetworkClientResponse
    {
        public DateTime StartTs { get; set; }
        public DateTime EndTs { get; set; }
        public int AvgLatencyMs { get; set; }
    }
}