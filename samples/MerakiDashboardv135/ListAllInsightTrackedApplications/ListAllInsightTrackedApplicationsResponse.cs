using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<List<ListAllInsightTrackedApplicationsResponse>>(myJsonResponse);
    public class ListAllInsightTrackedApplicationsResponse
    {
        public string ApplicationId { get; set; }
        public string Name { get; set; }
        public Thresholds Thresholds { get; set; }
    }

    public class ByNetwork
    {
        public string NetworkId { get; set; }
        public int Goodput { get; set; }
        public int ResponseDuration { get; set; }
    }

    public class Thresholds
    {
        public string Type { get; set; }
        public List<ByNetwork> ByNetwork { get; set; }
    }
}