using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<PliancessortedbyutilizationovergiventimerangeResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class PliancessortedbyutilizationovergiventimerangeResponse
    {
        public Network Network { get; set; }
        public string Name { get; set; }
        public string Mac { get; set; }
        public string Serial { get; set; }
        public string Model { get; set; }
        public Utilization Utilization { get; set; }
    }

    public class Average
    {
        public double Percentage { get; set; }
    }

    public class Utilization
    {
        public Average Average { get; set; }
    }
}