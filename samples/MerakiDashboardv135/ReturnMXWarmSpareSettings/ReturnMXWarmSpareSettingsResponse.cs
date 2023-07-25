using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<ReturnMXWarmSpareSettingsResponse>(myJsonResponse);
    public class ReturnMXWarmSpareSettingsResponse
    {
        public bool Enabled { get; set; }
        public string PrimarySerial { get; set; }
        public string SpareSerial { get; set; }
        public string UplinkMode { get; set; }
        public Wan2 Wan1 { get; set; }
        public Wan1 Wan2 { get; set; }
    }

    public class Wan2
    {
        public string Ip { get; set; }
        public string Subnet { get; set; }
    }
}