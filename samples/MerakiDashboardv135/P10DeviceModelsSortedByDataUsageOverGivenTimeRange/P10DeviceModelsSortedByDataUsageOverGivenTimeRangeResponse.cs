using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<P10DeviceModelsSortedByDataUsageOverGivenTimeRangeResponse>>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class P10DeviceModelsSortedByDataUsageOverGivenTimeRangeResponse
    {
        public string Model { get; set; }
        public int Count { get; set; }
        public Usage5 Usage { get; set; }
    }

    public class Usage5
    {
        public int Total { get; set; }
        public int Average { get; set; }
    }
}