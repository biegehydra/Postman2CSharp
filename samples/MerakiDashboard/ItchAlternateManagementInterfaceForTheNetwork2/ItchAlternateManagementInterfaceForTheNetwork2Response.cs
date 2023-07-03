using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<ItchAlternateManagementInterfaceForTheNetwork2Response>(myJsonResponse);
namespace MerakiDashboard
{
    public class ItchAlternateManagementInterfaceForTheNetwork2Response
    {
        public bool Enabled { get; set; }
        public int VlanId { get; set; }
        public List<string> Protocols { get; set; }
        public List<Switches> Switches { get; set; }
    }
}