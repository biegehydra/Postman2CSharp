using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<Stop10switchesbyenergyusageovergiventimerangeResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class Stop10switchesbyenergyusageovergiventimerangeResponse
    {
        public Network Network { get; set; }
        public string Name { get; set; }
        public string Mac { get; set; }
        public string Model { get; set; }
        public Usage10 Usage { get; set; }
    }

    public class Usage10
    {
        public double Total { get; set; }
    }
}