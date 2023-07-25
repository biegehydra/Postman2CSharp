using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<List<OnsTop10DevicesSortedByDataUsageOverGivenTimeRangeResponse>>(myJsonResponse);
    public class OnsTop10DevicesSortedByDataUsageOverGivenTimeRangeResponse
    {
        public string Name { get; set; }
        public string Model { get; set; }
        public string Serial { get; set; }
        public string Mac { get; set; }
        public string ProductType { get; set; }
        public ListSwitchPortSchedulesResponse Network { get; set; }
        public Usage4 Usage { get; set; }
        public AdaptivePolicyAggregateStatisticsForAnOrganizationResponse Clients { get; set; }
    }

    public class Usage4
    {
        public double Total { get; set; }
        public double Percentage { get; set; }
    }
}