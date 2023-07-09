using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<IentCountsOverTimeForANetworkDeviceOrNetworkClientResponse>>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class IentCountsOverTimeForANetworkDeviceOrNetworkClientResponse
    {
        public DateTime StartTs { get; set; }
        public DateTime EndTs { get; set; }
        public int ClientCount { get; set; }
    }
}