using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateTheSiteToSiteVPNSettingsOfANetworkRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdateTheSiteToSiteVPNSettingsOfANetworkRequest
    {
        public string Mode { get; set; }
        public List<Hubs2> Hubs { get; set; }
        public List<Subnets2> Subnets { get; set; }
    }

    public class Hubs2
    {
        public string HubId { get; set; }
        public string UseDefaultRoute { get; set; }
    }

    public class Subnets2
    {
        public string LocalSubnet { get; set; }
        public string UseVpn { get; set; }
    }
}