using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<GeInMbOverGivenTimeRangeGroupedByManufacturerResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class GeInMbOverGivenTimeRangeGroupedByManufacturerResponse
    {
        public string Name { get; set; }
        public Clients Clients { get; set; }
        public Usage6 Usage { get; set; }
    }

    public class Usage6
    {
        public int Total { get; set; }
        public int Upstream { get; set; }
        public int Downstream { get; set; }
    }
}