using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<TsinthegivenorganizationwithinagiventimerangeResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class TsinthegivenorganizationwithinagiventimerangeResponse
    {
        public DateTime Ts { get; set; }
        public int Total { get; set; }
        public int Upstream { get; set; }
        public int Downstream { get; set; }
    }
}