using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<ItchAlternateManagementInterfaceForTheNetworkResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class ItchAlternateManagementInterfaceForTheNetworkResponse
    {
        public bool Enabled { get; set; }
        public int VlanId { get; set; }
        public List<string> Protocols { get; set; }
        public List<Switches> Switches { get; set; }
    }

    public class Switches
    {
        public string Serial { get; set; }
        public string AlternateManagementIp { get; set; }
        public string SubnetMask { get; set; }
        public string Gateway { get; set; }
    }
}