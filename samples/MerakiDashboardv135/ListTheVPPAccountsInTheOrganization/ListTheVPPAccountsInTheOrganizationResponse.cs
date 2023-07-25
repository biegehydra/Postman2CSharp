using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<List<ListTheVPPAccountsInTheOrganizationResponse>>(myJsonResponse);
    public class ListTheVPPAccountsInTheOrganizationResponse
    {
        public string Id { get; set; }
        public string VppServiceToken { get; set; }
    }
}