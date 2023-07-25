using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<RoundClientDataUsageInMbAcrossTheGivenOrganizationResponse>(myJsonResponse);
    public class RoundClientDataUsageInMbAcrossTheGivenOrganizationResponse
    {
        public Usage2 Usage { get; set; }
        public ClientsInTheGivenOrganizationWithinAGivenTimeRangeResponse Counts { get; set; }
    }

    public class Usage2
    {
        public ClientsInTheGivenOrganizationWithinAGivenTimeRangeResponse Overall { get; set; }
        public double Average { get; set; }
    }
}