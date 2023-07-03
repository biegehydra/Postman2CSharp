using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<SwapMXPrimaryAndWarmSpareAppliancesResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class SwapMXPrimaryAndWarmSpareAppliancesResponse
    {
        public bool Enabled { get; set; }
        public string PrimarySerial { get; set; }
        public string SpareSerial { get; set; }
        public string UplinkMode { get; set; }
        public Wan1 Wan1 { get; set; }
        public Wan2 Wan2 { get; set; }
    }
}