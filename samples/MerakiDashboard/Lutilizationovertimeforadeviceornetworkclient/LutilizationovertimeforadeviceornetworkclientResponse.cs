using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<LutilizationovertimeforadeviceornetworkclientResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class LutilizationovertimeforadeviceornetworkclientResponse
    {
        public DateTime StartTs { get; set; }
        public DateTime EndTs { get; set; }
        public double UtilizationTotal { get; set; }
        public double Utilization80211 { get; set; }
        public double UtilizationNon80211 { get; set; }
    }
}