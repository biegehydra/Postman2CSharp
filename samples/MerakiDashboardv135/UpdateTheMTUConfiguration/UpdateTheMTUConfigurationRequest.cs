using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<UpdateTheMTUConfigurationRequest>(myJsonResponse);
    public class UpdateTheMTUConfigurationRequest
    {
        public string DefaultMtuSize { get; set; }
        public List<Overrides3> Overrides { get; set; }
    }

    public class Overrides3
    {
        public string MtuSize { get; set; }
        public List<string> Switches { get; set; }
        public List<string> SwitchProfiles { get; set; }
    }
}