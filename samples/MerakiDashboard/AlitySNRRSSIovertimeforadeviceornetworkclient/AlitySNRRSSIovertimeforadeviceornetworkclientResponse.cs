using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<AlitySNRRSSIOverTimeForADeviceOrNetworkClientResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class AlitySNRRSSIOverTimeForADeviceOrNetworkClientResponse
    {
        public DateTime StartTs { get; set; }
        public DateTime EndTs { get; set; }
        public int Snr { get; set; }
        public int Rssi { get; set; }
    }
}