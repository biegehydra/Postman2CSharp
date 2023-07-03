using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateWarmSpareConfigurationForASwitchRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdateWarmSpareConfigurationForASwitchRequest
    {
        public string Enabled { get; set; }
        public string SpareSerial { get; set; }
    }
}