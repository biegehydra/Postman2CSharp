using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<HannelUtilizationOverTimeForADeviceOrNetworkClientResponse>>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class HannelUtilizationOverTimeForADeviceOrNetworkClientResponse
    {
        public DateTime StartTs { get; set; }
        public DateTime EndTs { get; set; }
        public double UtilizationTotal { get; set; }
        public double Utilization80211 { get; set; }
        public double UtilizationNon80211 { get; set; }
    }
}