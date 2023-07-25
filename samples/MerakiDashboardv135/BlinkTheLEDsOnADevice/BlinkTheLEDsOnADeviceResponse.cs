using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<BlinkTheLEDsOnADeviceResponse>(myJsonResponse);
    public class BlinkTheLEDsOnADeviceResponse
    {
        public int Duration { get; set; }
        public int Period { get; set; }
        public int Duty { get; set; }
    }
}