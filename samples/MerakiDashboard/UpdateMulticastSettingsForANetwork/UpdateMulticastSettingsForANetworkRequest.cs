using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateMulticastSettingsForANetworkRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdateMulticastSettingsForANetworkRequest
    {
        public DefaultSettings2 DefaultSettings { get; set; }
        public List<Overrides2> Overrides { get; set; }
    }

    public class Overrides2
    {
        public string IgmpSnoopingEnabled { get; set; }
        public string FloodUnknownMulticastTrafficEnabled { get; set; }
        public List<string> SwitchProfiles { get; set; }
        public List<string> Switches { get; set; }
        public List<string> Stacks { get; set; }
    }

    public class DefaultSettings2
    {
        public string IgmpSnoopingEnabled { get; set; }
        public string FloodUnknownMulticastTrafficEnabled { get; set; }
    }
}