using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<RoundClientDataUsageInMbAcrossTheGivenOrganizationResponse>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class RoundClientDataUsageInMbAcrossTheGivenOrganizationResponse
    {
        public Usage2 Usage { get; set; }
        public Counts5 Counts { get; set; }
    }

    public class Overall
    {
        public int Total { get; set; }
        public int Downstream { get; set; }
        public int Upstream { get; set; }
    }

    public class Usage2
    {
        public Overall Overall { get; set; }
        public double Average { get; set; }
    }
}