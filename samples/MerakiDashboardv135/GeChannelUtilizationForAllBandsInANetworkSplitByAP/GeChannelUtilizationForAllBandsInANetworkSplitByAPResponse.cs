using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<List<GeChannelUtilizationForAllBandsInANetworkSplitByAPResponse>>(myJsonResponse);
    public class GeChannelUtilizationForAllBandsInANetworkSplitByAPResponse
    {
        public string Serial { get; set; }
        public string Mac { get; set; }
        public ListLinkAggregationGroupsResponse Network { get; set; }
        public List<ByBand> ByBand { get; set; }
    }

    public class ByBand
    {
        public string Band { get; set; }
        public NonWifi Wifi { get; set; }
        public NonWifi NonWifi { get; set; }
        public Battery Total { get; set; }
    }

    public class NonWifi
    {
        public double Percentage { get; set; }
    }
}