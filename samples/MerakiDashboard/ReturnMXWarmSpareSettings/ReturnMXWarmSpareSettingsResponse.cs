using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<ReturnMXWarmSpareSettingsResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class ReturnMXWarmSpareSettingsResponse
    {
        public bool Enabled { get; set; }
        public string PrimarySerial { get; set; }
        public string SpareSerial { get; set; }
        public string UplinkMode { get; set; }
        public Wan1 Wan1 { get; set; }
        public Wan2 Wan2 { get; set; }
    }

    public class Wan14
    {
        public string Ip { get; set; }
        public string Subnet { get; set; }
    }

    public class Wan24
    {
        public string Ip { get; set; }
        public string Subnet { get; set; }
    }
}