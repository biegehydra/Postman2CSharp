using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<List<IentCountsOverTimeForANetworkDeviceOrNetworkClientResponse>>(myJsonResponse);
    public class IentCountsOverTimeForANetworkDeviceOrNetworkClientResponse
    {
        public DateTime StartTs { get; set; }
        public DateTime EndTs { get; set; }
        public int ClientCount { get; set; }
    }
}