using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<MoveASetOfDevicesToANewNetworkResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class MoveASetOfDevicesToANewNetworkResponse
    {
        public List<string> Ids { get; set; }
        public string NewNetwork { get; set; }
    }
}