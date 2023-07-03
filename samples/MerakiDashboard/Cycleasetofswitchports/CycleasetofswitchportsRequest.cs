using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<CycleasetofswitchportsRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class CycleasetofswitchportsRequest
    {
        public List<string> Ports { get; set; }
    }
}