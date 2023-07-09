using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<MoveASetOfDevicesToANewNetworkRequest>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class MoveASetOfDevicesToANewNetworkRequest
    {
        public string NewNetwork { get; set; }
        public List<string> WifiMacs { get; set; }
        public List<string> Ids { get; set; }
        public List<string> Serials { get; set; }
        public List<string> Scope { get; set; }
    }
}