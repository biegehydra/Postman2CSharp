using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<ReatesANewCameraWirelessProfileForThisNetworkResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class ReatesANewCameraWirelessProfileForThisNetworkResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int AppliedDeviceCount { get; set; }
        public Ssid Ssid { get; set; }
        public Authentication4 Identity { get; set; }
    }
}