using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<OnsTop10DevicesSortedByDataUsageOverGivenTimeRangeResponse>>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class OnsTop10DevicesSortedByDataUsageOverGivenTimeRangeResponse
    {
        public string Name { get; set; }
        public string Model { get; set; }
        public string Serial { get; set; }
        public string Mac { get; set; }
        public string ProductType { get; set; }
        public Network Network { get; set; }
        public Usage4 Usage { get; set; }
        public Clients2 Clients { get; set; }
    }

    public class Usage4
    {
        public double Total { get; set; }
        public double Percentage { get; set; }
    }
}