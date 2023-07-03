using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<EgatedLatencyInfoForAGivenClientOnThisNetworkResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class EgatedLatencyInfoForAGivenClientOnThisNetworkResponse
    {
        public string Mac { get; set; }
        public LatencyStats LatencyStats { get; set; }
    }
}