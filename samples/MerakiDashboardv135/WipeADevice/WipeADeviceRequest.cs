using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<WipeADeviceRequest>(myJsonResponse);
    public class WipeADeviceRequest
    {
        public string WifiMac { get; set; }
        public string Id { get; set; }
        public string Serial { get; set; }
        public string Pin { get; set; }
    }
}