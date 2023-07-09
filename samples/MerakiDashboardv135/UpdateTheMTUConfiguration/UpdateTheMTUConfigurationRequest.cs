using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateTheMTUConfigurationRequest>(myJsonResponse);
namespace MerakiDashboardv135
{
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