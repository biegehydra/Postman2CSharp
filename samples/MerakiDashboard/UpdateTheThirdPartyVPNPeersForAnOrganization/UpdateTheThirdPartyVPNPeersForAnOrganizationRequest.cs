using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<UpdateTheThirdPartyVPNPeersForAnOrganizationRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class UpdateTheThirdPartyVPNPeersForAnOrganizationRequest
    {
        public List<Peers> Peers { get; set; }
    }
}