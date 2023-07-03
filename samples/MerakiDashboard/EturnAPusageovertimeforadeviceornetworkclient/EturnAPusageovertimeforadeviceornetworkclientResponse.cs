using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<EturnAPUsageOverTimeForADeviceOrNetworkClientResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class EturnAPUsageOverTimeForADeviceOrNetworkClientResponse
    {
        public DateTime StartTs { get; set; }
        public DateTime EndTs { get; set; }
        public int TotalKbps { get; set; }
        public int SentKbps { get; set; }
        public int ReceivedKbps { get; set; }
    }
}