using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<EFirewallRulesForAnOrganizationsSiteToSiteVPNResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class EFirewallRulesForAnOrganizationsSiteToSiteVPNResponse
    {
        public List<Rules> Rules { get; set; }
    }
}