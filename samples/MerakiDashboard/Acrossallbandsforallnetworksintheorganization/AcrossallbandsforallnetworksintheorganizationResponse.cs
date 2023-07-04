using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<AcrossAllBandsForAllNetworksInTheOrganizationResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class AcrossAllBandsForAllNetworksInTheOrganizationResponse
    {
        public Profile3 Network { get; set; }
        public List<ByBand> ByBand { get; set; }
    }
}