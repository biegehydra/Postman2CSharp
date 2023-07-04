using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<STop10SwitchesByEnergyUsageOverGivenTimeRangeResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class STop10SwitchesByEnergyUsageOverGivenTimeRangeResponse
    {
        public ResultingNetworks Network { get; set; }
        public string Name { get; set; }
        public string Mac { get; set; }
        public string Model { get; set; }
        public Usage Usage { get; set; }
    }
}