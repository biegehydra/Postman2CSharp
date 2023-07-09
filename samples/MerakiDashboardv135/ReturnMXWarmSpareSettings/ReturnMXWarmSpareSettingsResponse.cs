using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<ReturnMXWarmSpareSettingsResponse>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class ReturnMXWarmSpareSettingsResponse
    {
        public bool Enabled { get; set; }
        public string PrimarySerial { get; set; }
        public string SpareSerial { get; set; }
        public string UplinkMode { get; set; }
        public Wan3 Wan1 { get; set; }
        public Wan4 Wan2 { get; set; }
    }

    public class Wan3
    {
        public string Ip { get; set; }
        public string Subnet { get; set; }
    }

    public class Wan4
    {
        public string Ip { get; set; }
        public string Subnet { get; set; }
    }
}