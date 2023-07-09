using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<WipeADeviceRequest>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class WipeADeviceRequest
    {
        public string WifiMac { get; set; }
        public string Id { get; set; }
        public string Serial { get; set; }
        public string Pin { get; set; }
    }
}