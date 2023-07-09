using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<ListLinkAggregationGroupsResponse>>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class ListLinkAggregationGroupsResponse
    {
        public string Id { get; set; }
        public List<SwitchPorts> SwitchPorts { get; set; }
    }

    public class SwitchPorts
    {
        public string Serial { get; set; }
        public string PortId { get; set; }
    }
}