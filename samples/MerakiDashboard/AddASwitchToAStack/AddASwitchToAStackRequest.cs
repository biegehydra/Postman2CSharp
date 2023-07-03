using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<AddASwitchToAStackRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class AddASwitchToAStackRequest
    {
        public string Serial { get; set; }
    }
}