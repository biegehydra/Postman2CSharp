using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<BlinkTheLEDsOnADeviceRequest>(myJsonResponse);
    public class BlinkTheLEDsOnADeviceRequest
    {
        public string Duration { get; set; }
        public string Period { get; set; }
        public string Duty { get; set; }
    }
}