using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<ListTheSwitchStacksInANetworkResponse>>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class ListTheSwitchStacksInANetworkResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<string> Serials { get; set; }
    }
}