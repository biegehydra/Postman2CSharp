using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<RatesovertimeforanetworkdeviceornetworkclientResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class RatesovertimeforanetworkdeviceornetworkclientResponse
    {
        public DateTime StartTs { get; set; }
        public DateTime EndTs { get; set; }
        public int AverageKbps { get; set; }
        public int DownloadKbps { get; set; }
        public int UploadKbps { get; set; }
    }
}