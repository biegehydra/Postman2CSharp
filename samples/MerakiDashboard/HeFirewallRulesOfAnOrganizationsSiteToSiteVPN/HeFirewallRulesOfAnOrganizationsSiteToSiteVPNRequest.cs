using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<HeFirewallRulesOfAnOrganizationsSiteToSiteVPNRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class HeFirewallRulesOfAnOrganizationsSiteToSiteVPNRequest
    {
        public List<Rules> Rules { get; set; }
        public string SyslogDefaultRule { get; set; }
    }
}