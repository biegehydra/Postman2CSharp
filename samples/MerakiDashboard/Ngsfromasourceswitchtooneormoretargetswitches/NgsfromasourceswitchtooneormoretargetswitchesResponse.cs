using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<NgsfromasourceswitchtooneormoretargetswitchesResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class NgsfromasourceswitchtooneormoretargetswitchesResponse
    {
        public string SourceSerial { get; set; }
        public List<string> TargetSerials { get; set; }
    }
}