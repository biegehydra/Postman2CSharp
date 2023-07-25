using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<TheSwitchAlternateManagementInterfaceForTheNetwork2Request>(myJsonResponse);
    public class TheSwitchAlternateManagementInterfaceForTheNetwork2Request
    {
        public string Enabled { get; set; }
        public string VlanId { get; set; }
        public List<string> Protocols { get; set; }
        public List<Switches> Switches { get; set; }
    }
}