using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<BlinkTheLEDsOnADeviceResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class BlinkTheLEDsOnADeviceResponse
    {
        public int Duration { get; set; }
        public int Period { get; set; }
        public int Duty { get; set; }
    }
}