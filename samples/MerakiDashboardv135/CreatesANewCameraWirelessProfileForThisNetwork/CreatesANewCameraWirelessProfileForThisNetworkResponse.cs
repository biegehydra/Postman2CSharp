using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<CreatesANewCameraWirelessProfileForThisNetworkResponse>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class CreatesANewCameraWirelessProfileForThisNetworkResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int AppliedDeviceCount { get; set; }
        public Ssid Ssid { get; set; }
        public Identity Identity { get; set; }
    }
}