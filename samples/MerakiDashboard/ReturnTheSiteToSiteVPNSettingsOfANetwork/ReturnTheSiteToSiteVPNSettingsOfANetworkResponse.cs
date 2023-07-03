using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<ReturnTheSiteToSiteVPNSettingsOfANetworkResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class ReturnTheSiteToSiteVPNSettingsOfANetworkResponse
    {
        public string Mode { get; set; }
        public List<Hubs> Hubs { get; set; }
        public List<Subnets> Subnets { get; set; }
    }

    public class Hubs
    {
        public string HubId { get; set; }
        public bool UseDefaultRoute { get; set; }
    }

    public class Subnets
    {
        public string LocalSubnet { get; set; }
        public bool UseVpn { get; set; }
    }
}