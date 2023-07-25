using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<List<ReturnAPUsageOverTimeForADeviceOrNetworkClientResponse>>(myJsonResponse);
    public class ReturnAPUsageOverTimeForADeviceOrNetworkClientResponse
    {
        public DateTime StartTs { get; set; }
        public DateTime EndTs { get; set; }
        public int TotalKbps { get; set; }
        public int SentKbps { get; set; }
        public int ReceivedKbps { get; set; }
    }
}