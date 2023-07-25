using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<ListWirelessMeshStatusesForRepeatersResponse>(myJsonResponse);
    public class ListWirelessMeshStatusesForRepeatersResponse
    {
        public string Serial { get; set; }
        public List<string> MeshRoute { get; set; }
        public LatestMeshPerformance LatestMeshPerformance { get; set; }
    }

    public class LatestMeshPerformance
    {
        public int Mbps { get; set; }
        public int Metric { get; set; }
        public string UsagePercentage { get; set; }
    }
}