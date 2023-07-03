using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<RemoveASwitchFromAStackRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class RemoveASwitchFromAStackRequest
    {
        public string Serial { get; set; }
    }
}