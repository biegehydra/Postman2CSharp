using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<EAnExistingCameraWirelessProfileInThisNetworkRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class EAnExistingCameraWirelessProfileInThisNetworkRequest
    {
        public string Name { get; set; }
        public Ssid Ssid { get; set; }
        public Identity Identity { get; set; }
    }
}