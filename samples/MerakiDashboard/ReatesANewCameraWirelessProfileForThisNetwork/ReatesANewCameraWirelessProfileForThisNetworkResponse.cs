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
        public Ssid2 Ssid { get; set; }
        public Identity Identity { get; set; }
    }

    public class Ssid2
    {
        public string Name { get; set; }
        public string AuthMode { get; set; }
        public string EncryptionMode { get; set; }
    }
}