using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<List<ListTheSwitchStacksInANetworkResponse>>(myJsonResponse);
    public class ListTheSwitchStacksInANetworkResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<string> Serials { get; set; }
    }
}