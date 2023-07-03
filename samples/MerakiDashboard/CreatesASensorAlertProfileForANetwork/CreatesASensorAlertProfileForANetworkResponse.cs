using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<CreatesASensorAlertProfileForANetworkResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class CreatesASensorAlertProfileForANetworkResponse
    {
        public string ProfileId { get; set; }
        public string Name { get; set; }
        public Schedule Schedule { get; set; }
        public List<Conditions> Conditions { get; set; }
        public Recipients Recipients { get; set; }
        public List<string> Serials { get; set; }
    }
}