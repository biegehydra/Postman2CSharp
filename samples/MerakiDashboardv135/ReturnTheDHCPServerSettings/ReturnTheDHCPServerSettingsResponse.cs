using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<ReturnTheDHCPServerSettingsResponse>(myJsonResponse);
    public class ReturnTheDHCPServerSettingsResponse
    {
        public Alerts Alerts { get; set; }
        public string DefaultPolicy { get; set; }
        public List<string> BlockedServers { get; set; }
        public List<string> AllowedServers { get; set; }
        public Authentication2 ArpInspection { get; set; }
    }

    public class Alerts
    {
        public Authentication2 Email { get; set; }
    }
}