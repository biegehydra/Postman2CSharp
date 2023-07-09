using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<ReturnTheDHCPServerSettingsResponse>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class ReturnTheDHCPServerSettingsResponse
    {
        public Alerts Alerts { get; set; }
        public string DefaultPolicy { get; set; }
        public List<string> BlockedServers { get; set; }
        public List<string> AllowedServers { get; set; }
        public ArpInspection ArpInspection { get; set; }
    }

    public class Alerts
    {
        public Email Email { get; set; }
    }

    public class ArpInspection
    {
        public bool Enabled { get; set; }
    }

    public class Email
    {
        public bool Enabled { get; set; }
    }
}