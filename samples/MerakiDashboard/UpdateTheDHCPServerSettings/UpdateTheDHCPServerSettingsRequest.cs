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
        public ArpInspection2 ArpInspection { get; set; }
    }

    public class ArpInspection2
    {
        public string Enabled { get; set; }
    }

    public class Email2
    {
        public string Enabled { get; set; }
    }
}