using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<ReturnPerPortVLANSettingsForASingleMXPortResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class ReturnPerPortVLANSettingsForASingleMXPortResponse
    {
        public int Number { get; set; }
        public bool Enabled { get; set; }
        public string Type { get; set; }
        public bool DropUntaggedTraffic { get; set; }
        public int Vlan { get; set; }
        public string AllowedVlans { get; set; }
        public string AccessPolicy { get; set; }
    }
}