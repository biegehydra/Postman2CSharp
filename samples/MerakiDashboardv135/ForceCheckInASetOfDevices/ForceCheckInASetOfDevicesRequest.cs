using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<ForceCheckInASetOfDevicesRequest>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class ForceCheckInASetOfDevicesRequest
    {
        public List<string> WifiMacs { get; set; }
        public List<string> Ids { get; set; }
        public List<string> Serials { get; set; }
        public List<string> Scope { get; set; }
    }
}