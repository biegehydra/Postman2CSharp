using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateTheMTUConfigurationRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdateTheMTUConfigurationRequest
    {
        public string DefaultMtuSize { get; set; }
        public List<Overrides5> Overrides { get; set; }
    }

    public class Overrides5
    {
        public string MtuSize { get; set; }
        public List<string> Switches { get; set; }
        public List<string> SwitchProfiles { get; set; }
    }
}