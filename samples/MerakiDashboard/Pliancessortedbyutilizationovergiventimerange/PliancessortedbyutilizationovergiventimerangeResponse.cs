using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<PliancesSortedByUtilizationOverGivenTimeRangeResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class PliancesSortedByUtilizationOverGivenTimeRangeResponse
    {
        public ResultingNetworks Network { get; set; }
        public string Name { get; set; }
        public string Mac { get; set; }
        public string Serial { get; set; }
        public string Model { get; set; }
        public Utilization Utilization { get; set; }
    }

    public class Utilization
    {
        public Wifi Average { get; set; }
    }
}