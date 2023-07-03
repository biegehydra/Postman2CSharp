using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<HeFirewallRulesOfAnOrganizationsSiteToSiteVPNResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class HeFirewallRulesOfAnOrganizationsSiteToSiteVPNResponse
    {
        public List<Rules> Rules { get; set; }
    }
}