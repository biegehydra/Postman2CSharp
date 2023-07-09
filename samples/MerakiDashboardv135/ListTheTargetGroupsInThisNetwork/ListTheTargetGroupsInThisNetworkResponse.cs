using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<ListTheTargetGroupsInThisNetworkResponse>>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class ListTheTargetGroupsInThisNetworkResponse
    {
        public string Name { get; set; }
        public string Scope { get; set; }
        public string Tags { get; set; }
        public string Type { get; set; }
    }
}