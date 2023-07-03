using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateTheSiteToSiteVPNSettingsOfANetworkResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdateTheSiteToSiteVPNSettingsOfANetworkResponse
    {
        public string Mode { get; set; }
        public List<Hubs> Hubs { get; set; }
        public List<Subnets> Subnets { get; set; }
    }
}