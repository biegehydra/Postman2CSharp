using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<RegatedLatencyInfoForThisNetworkGroupedByNodeResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class RegatedLatencyInfoForThisNetworkGroupedByNodeResponse
    {
        public string Serial { get; set; }
        public LatencyStats LatencyStats { get; set; }
    }
}