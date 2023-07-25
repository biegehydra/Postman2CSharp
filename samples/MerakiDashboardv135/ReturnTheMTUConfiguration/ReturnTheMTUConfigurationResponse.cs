using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<ReturnTheMTUConfigurationResponse>(myJsonResponse);
    public class ReturnTheMTUConfigurationResponse
    {
        public int DefaultMtuSize { get; set; }
        public List<Overrides2> Overrides { get; set; }
    }

    public class Overrides2
    {
        public List<string> Switches { get; set; }
        public List<string> SwitchProfiles { get; set; }
        public int MtuSize { get; set; }
    }
}