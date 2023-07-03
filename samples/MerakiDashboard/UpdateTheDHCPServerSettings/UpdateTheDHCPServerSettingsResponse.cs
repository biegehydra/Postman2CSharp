using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateTheDHCPServerSettingsResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdateTheDHCPServerSettingsResponse
    {
        public Alerts Alerts { get; set; }
        public string DefaultPolicy { get; set; }
        public List<string> BlockedServers { get; set; }
        public List<string> AllowedServers { get; set; }
        public ArpInspection3 ArpInspection { get; set; }
    }

    public class ArpInspection3
    {
        public bool Enabled { get; set; }
        public List<string> UnsupportedModels { get; set; }
    }
}