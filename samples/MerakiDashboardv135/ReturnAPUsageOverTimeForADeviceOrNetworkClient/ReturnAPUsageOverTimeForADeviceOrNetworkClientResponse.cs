using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<ReturnAPUsageOverTimeForADeviceOrNetworkClientResponse>>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class ReturnAPUsageOverTimeForADeviceOrNetworkClientResponse
    {
        public DateTime StartTs { get; set; }
        public DateTime EndTs { get; set; }
        public int TotalKbps { get; set; }
        public int SentKbps { get; set; }
        public int ReceivedKbps { get; set; }
    }
}