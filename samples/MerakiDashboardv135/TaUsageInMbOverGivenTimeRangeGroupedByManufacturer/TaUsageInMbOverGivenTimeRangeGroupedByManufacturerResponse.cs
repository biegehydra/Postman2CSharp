using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<TaUsageInMbOverGivenTimeRangeGroupedByManufacturerResponse>>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class TaUsageInMbOverGivenTimeRangeGroupedByManufacturerResponse
    {
        public string Name { get; set; }
        public Clients2 Clients { get; set; }
        public Usage3 Usage { get; set; }
    }
}