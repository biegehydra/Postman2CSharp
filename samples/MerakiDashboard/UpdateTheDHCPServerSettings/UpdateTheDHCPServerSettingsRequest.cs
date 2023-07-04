using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateTheDHCPServerSettingsRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdateTheDHCPServerSettingsRequest
    {
        public Alerts Alerts { get; set; }
        public string DefaultPolicy { get; set; }
        public List<string> AllowedServers { get; set; }
        public List<string> BlockedServers { get; set; }
        public VlanTagging3 ArpInspection { get; set; }
    }
}