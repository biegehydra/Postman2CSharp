using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<List<YDataRatesOverTimeForANetworkDeviceOrNetworkClientResponse>>(myJsonResponse);
    public class YDataRatesOverTimeForANetworkDeviceOrNetworkClientResponse
    {
        public DateTime StartTs { get; set; }
        public DateTime EndTs { get; set; }
        public int AverageKbps { get; set; }
        public int DownloadKbps { get; set; }
        public int UploadKbps { get; set; }
    }
}