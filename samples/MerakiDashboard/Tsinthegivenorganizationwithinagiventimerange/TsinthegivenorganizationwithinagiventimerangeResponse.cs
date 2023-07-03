using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<TsInTheGivenOrganizationWithinAGivenTimeRangeResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class TsInTheGivenOrganizationWithinAGivenTimeRangeResponse
    {
        public DateTime Ts { get; set; }
        public int Total { get; set; }
        public int Upstream { get; set; }
        public int Downstream { get; set; }
    }
}