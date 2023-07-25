using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<List<TionsTop10ClientsByDataUsageInMbOverGivenTimeRangeResponse>>(myJsonResponse);
    public class TionsTop10ClientsByDataUsageInMbOverGivenTimeRangeResponse
    {
        public string Name { get; set; }
        public string Mac { get; set; }
        public string Id { get; set; }
        public ListSwitchPortSchedulesResponse Network { get; set; }
        public Usage3 Usage { get; set; }
    }

    public class Usage3
    {
        public int Total { get; set; }
        public int Upstream { get; set; }
        public int Downstream { get; set; }
        public double Percentage { get; set; }
    }
}