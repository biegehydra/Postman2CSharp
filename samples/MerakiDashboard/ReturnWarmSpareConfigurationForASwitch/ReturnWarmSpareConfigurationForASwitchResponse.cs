using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<ReturnWarmSpareConfigurationForASwitchResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class ReturnWarmSpareConfigurationForASwitchResponse
    {
        public bool Enabled { get; set; }
        public string PrimarySerial { get; set; }
        public string SpareSerial { get; set; }
    }
}