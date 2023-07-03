using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<AddASwitchToAStackResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class AddASwitchToAStackResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<string> Serials { get; set; }
    }
}