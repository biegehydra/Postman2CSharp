using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<ReturnTheMTUConfigurationResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class ReturnTheMTUConfigurationResponse
    {
        public int DefaultMtuSize { get; set; }
        public List<Overrides4> Overrides { get; set; }
    }

    public class Overrides4
    {
        public List<string> Switches { get; set; }
        public List<string> SwitchProfiles { get; set; }
        public int MtuSize { get; set; }
    }
}