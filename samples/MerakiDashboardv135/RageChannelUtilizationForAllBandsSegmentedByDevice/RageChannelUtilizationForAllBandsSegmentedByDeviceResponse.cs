using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<List<RageChannelUtilizationForAllBandsSegmentedByDeviceResponse>>(myJsonResponse);
    public class RageChannelUtilizationForAllBandsSegmentedByDeviceResponse
    {
        public DateTime StartTs { get; set; }
        public DateTime EndTs { get; set; }
        public string Serial { get; set; }
        public string Mac { get; set; }
        public ListLinkAggregationGroupsResponse Network { get; set; }
        public List<ByBand> ByBand { get; set; }
    }
}