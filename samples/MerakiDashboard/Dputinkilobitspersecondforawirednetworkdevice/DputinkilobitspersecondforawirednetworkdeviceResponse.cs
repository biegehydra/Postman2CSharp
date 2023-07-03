using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<DputinkilobitspersecondforawirednetworkdeviceResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class DputinkilobitspersecondforawirednetworkdeviceResponse
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double LossPercent { get; set; }
        public double LatencyMs { get; set; }
        public int Goodput { get; set; }
        public double Jitter { get; set; }
    }
}