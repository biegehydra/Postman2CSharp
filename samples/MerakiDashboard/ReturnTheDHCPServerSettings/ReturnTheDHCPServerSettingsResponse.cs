using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<ReturnTheDHCPServerSettingsResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class ReturnTheDHCPServerSettingsResponse
    {
        public Alerts Alerts { get; set; }
        public string DefaultPolicy { get; set; }
        public List<string> BlockedServers { get; set; }
        public List<string> AllowedServers { get; set; }
        public Wan2 ArpInspection { get; set; }
    }

    public class Alerts
    {
        public Wan2 Email { get; set; }
    }
}