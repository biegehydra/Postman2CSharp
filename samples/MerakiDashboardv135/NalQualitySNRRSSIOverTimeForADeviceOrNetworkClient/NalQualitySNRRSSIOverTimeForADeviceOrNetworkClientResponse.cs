using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<List<NalQualitySNRRSSIOverTimeForADeviceOrNetworkClientResponse>>(myJsonResponse);
    public class NalQualitySNRRSSIOverTimeForADeviceOrNetworkClientResponse
    {
        public DateTime StartTs { get; set; }
        public DateTime EndTs { get; set; }
        public int Snr { get; set; }
        public int Rssi { get; set; }
    }
}