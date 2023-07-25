using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<UpdateMXWarmSpareSettingsRequest>(myJsonResponse);
    public class UpdateMXWarmSpareSettingsRequest
    {
        public string Enabled { get; set; }
        public string SpareSerial { get; set; }
        public string UplinkMode { get; set; }
        public string VirtualIp1 { get; set; }
        public string VirtualIp2 { get; set; }
    }
}