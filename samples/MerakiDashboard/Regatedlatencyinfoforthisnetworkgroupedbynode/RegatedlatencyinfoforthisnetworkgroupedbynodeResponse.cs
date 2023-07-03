using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<RegatedlatencyinfoforthisnetworkgroupedbynodeResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class RegatedlatencyinfoforthisnetworkgroupedbynodeResponse
    {
        public string Serial { get; set; }
        public LatencyStats LatencyStats { get; set; }
    }
}