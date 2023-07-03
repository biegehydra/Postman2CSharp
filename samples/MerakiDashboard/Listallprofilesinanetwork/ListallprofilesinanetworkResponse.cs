using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<ListallprofilesinanetworkResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class ListallprofilesinanetworkResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Scope { get; set; }
        public List<string> Tags { get; set; }
    }
}