using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<ListTheCameraWirelessProfilesForThisNetworkResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class ListTheCameraWirelessProfilesForThisNetworkResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int AppliedDeviceCount { get; set; }
        public Ssid Ssid { get; set; }
        public Identity Identity { get; set; }
    }
}