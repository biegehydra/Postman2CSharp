using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<ListWirelessMeshStatusesForRepeatersResponse>(myJsonResponse);
namespace MerakiDashboardv135
{
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