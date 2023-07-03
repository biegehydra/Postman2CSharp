using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<CycleasetofswitchportsResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class CycleasetofswitchportsResponse
    {
        public List<string> Ports { get; set; }
    }
}