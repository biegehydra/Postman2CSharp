using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateMXWarmSpareSettingsRequest>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class UpdateMXWarmSpareSettingsRequest
    {
        public string Enabled { get; set; }
        public string SpareSerial { get; set; }
        public string UplinkMode { get; set; }
        public string VirtualIp1 { get; set; }
        public string VirtualIp2 { get; set; }
    }
}