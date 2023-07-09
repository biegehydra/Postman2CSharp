using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<BlinkTheLEDsOnADeviceRequest>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class BlinkTheLEDsOnADeviceRequest
    {
        public string Duration { get; set; }
        public string Period { get; set; }
        public string Duty { get; set; }
    }
}