using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<TheSwitchAlternateManagementInterfaceForTheNetwork2Request>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class TheSwitchAlternateManagementInterfaceForTheNetwork2Request
    {
        public string Enabled { get; set; }
        public string VlanId { get; set; }
        public List<string> Protocols { get; set; }
        public List<Switches> Switches { get; set; }
    }
}