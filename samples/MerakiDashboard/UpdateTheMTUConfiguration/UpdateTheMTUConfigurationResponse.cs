using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateTheMTUConfigurationResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdateTheMTUConfigurationResponse
    {
        public int DefaultMtuSize { get; set; }
        public List<Overrides> Overrides { get; set; }
    }
}