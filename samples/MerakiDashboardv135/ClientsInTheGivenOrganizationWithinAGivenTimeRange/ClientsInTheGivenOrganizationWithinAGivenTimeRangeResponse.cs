using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<List<ClientsInTheGivenOrganizationWithinAGivenTimeRangeResponse>>(myJsonResponse);
    public class ClientsInTheGivenOrganizationWithinAGivenTimeRangeResponse
    {
        public DateTime Ts { get; set; }
        public int Total { get; set; }
        public int Upstream { get; set; }
        public int Downstream { get; set; }
    }
}