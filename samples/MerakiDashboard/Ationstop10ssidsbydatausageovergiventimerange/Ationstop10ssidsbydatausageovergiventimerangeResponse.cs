using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<AtionsTop10SsidsByDataUsageOverGivenTimeRangeResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class AtionsTop10SsidsByDataUsageOverGivenTimeRangeResponse
    {
        public string Name { get; set; }
        public Usage Usage { get; set; }
        public Clients Clients { get; set; }
    }
}