using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<ReturnMulticastSettingsForANetworkResponse>(myJsonResponse);
    public class ReturnMulticastSettingsForANetworkResponse
    {
        public DefaultSettings DefaultSettings { get; set; }
        public List<Overrides> Overrides { get; set; }
    }

    public class Overrides
    {
        public List<string> Switches { get; set; }
        public List<string> Stacks { get; set; }
        public List<string> SwitchProfiles { get; set; }
        public bool IgmpSnoopingEnabled { get; set; }
        public bool FloodUnknownMulticastTrafficEnabled { get; set; }
    }

    public class DefaultSettings
    {
        public bool IgmpSnoopingEnabled { get; set; }
        public bool FloodUnknownMulticastTrafficEnabled { get; set; }
    }
}