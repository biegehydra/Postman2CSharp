using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<CreateAStackRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class CreateAStackRequest
    {
        public string Name { get; set; }
        public List<string> Serials { get; set; }
    }
}