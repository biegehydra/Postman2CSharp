using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<CreatesASensorAlertProfileForANetworkRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class CreatesASensorAlertProfileForANetworkRequest
    {
        public string Name { get; set; }
        public List<Conditions> Conditions { get; set; }
        public BlockedUrlCategories Schedule { get; set; }
        public Recipients Recipients { get; set; }
        public List<string> Serials { get; set; }
    }
}