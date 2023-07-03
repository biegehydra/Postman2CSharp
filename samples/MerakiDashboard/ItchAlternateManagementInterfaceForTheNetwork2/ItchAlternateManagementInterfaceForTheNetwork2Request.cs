using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<ItchAlternateManagementInterfaceForTheNetwork2Request>(myJsonResponse);
namespace MerakiDashboard
{
    public class ItchAlternateManagementInterfaceForTheNetwork2Request
    {
        public string Enabled { get; set; }
        public string VlanId { get; set; }
        public List<string> Protocols { get; set; }
        public List<Switches> Switches { get; set; }
    }
}