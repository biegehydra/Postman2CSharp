using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<List<ListTheTargetGroupsInThisNetworkResponse>>(myJsonResponse);
    public class ListTheTargetGroupsInThisNetworkResponse
    {
        public string Name { get; set; }
        public string Scope { get; set; }
        public string Tags { get; set; }
        public string Type { get; set; }
    }
}