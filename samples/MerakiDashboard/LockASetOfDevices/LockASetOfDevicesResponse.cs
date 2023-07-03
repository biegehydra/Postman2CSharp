using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<LockASetOfDevicesResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class LockASetOfDevicesResponse
    {
        public List<string> Ids { get; set; }
    }
}