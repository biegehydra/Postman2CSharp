using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<ForceCheckInASetOfDevicesResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class ForceCheckInASetOfDevicesResponse
    {
        public List<string> Ids { get; set; }
    }
}