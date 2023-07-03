using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateTheThirdPartyVPNPeersForAnOrganizationResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdateTheThirdPartyVPNPeersForAnOrganizationResponse
    {
        public List<Peers> Peers { get; set; }
    }
}