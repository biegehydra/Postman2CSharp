using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<LockASetOfDevicesRequest>(myJsonResponse);
    public class LockASetOfDevicesRequest
    {
        public List<string> WifiMacs { get; set; }
        public List<string> Ids { get; set; }
        public List<string> Serials { get; set; }
        public List<string> Scope { get; set; }
        public string Pin { get; set; }
    }
}