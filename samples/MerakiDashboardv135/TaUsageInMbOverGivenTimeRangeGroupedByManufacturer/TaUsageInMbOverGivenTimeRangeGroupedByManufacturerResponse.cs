using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<List<TaUsageInMbOverGivenTimeRangeGroupedByManufacturerResponse>>(myJsonResponse);
    public class TaUsageInMbOverGivenTimeRangeGroupedByManufacturerResponse
    {
        public string Name { get; set; }
        public AdaptivePolicyAggregateStatisticsForAnOrganizationResponse Clients { get; set; }
        public ClientsInTheGivenOrganizationWithinAGivenTimeRangeResponse Usage { get; set; }
    }
}