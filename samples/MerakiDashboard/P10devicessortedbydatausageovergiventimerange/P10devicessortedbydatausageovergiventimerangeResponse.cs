using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<P10DevicesSortedByDataUsageOverGivenTimeRangeResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class P10DevicesSortedByDataUsageOverGivenTimeRangeResponse
    {
        public string Name { get; set; }
        public string Model { get; set; }
        public string Serial { get; set; }
        public string Mac { get; set; }
        public string ProductType { get; set; }
        public Network Network { get; set; }
        public Usage7 Usage { get; set; }
        public Clients Clients { get; set; }
    }

    public class Usage7
    {
        public double Total { get; set; }
        public double Percentage { get; set; }
    }
}