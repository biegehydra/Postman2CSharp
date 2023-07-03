using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<ShowASwitchStackResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class ShowASwitchStackResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<string> Serials { get; set; }
    }
}