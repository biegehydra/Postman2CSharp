using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<CreateAStackResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class CreateAStackResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<string> Serials { get; set; }
    }
}