using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<ListTheMXSSIDsInANetworkResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class ListTheMXSSIDsInANetworkResponse
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public bool Enabled { get; set; }
        public int DefaultVlanId { get; set; }
        public string AuthMode { get; set; }
        public List<RadiusServers> RadiusServers { get; set; }
        public string EncryptionMode { get; set; }
        public string WpaEncryptionMode { get; set; }
        public bool Visible { get; set; }
    }

    public class RadiusServers
    {
        public string Host { get; set; }
        public int Port { get; set; }
    }
}