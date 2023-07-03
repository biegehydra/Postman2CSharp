using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<STop10SwitchesByEnergyUsageOverGivenTimeRangeResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class STop10SwitchesByEnergyUsageOverGivenTimeRangeResponse
    {
        public Network Network { get; set; }
        public string Name { get; set; }
        public string Mac { get; set; }
        public string Model { get; set; }
        public Usage10 Usage { get; set; }
    }

    public class Usage10
    {
        public double Total { get; set; }
    }
}