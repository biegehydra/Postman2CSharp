using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<List<P10DeviceModelsSortedByDataUsageOverGivenTimeRangeResponse>>(myJsonResponse);
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