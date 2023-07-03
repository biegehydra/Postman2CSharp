using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<Top10clientsbydatausageinmbovergiventimerangeResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class Top10clientsbydatausageinmbovergiventimerangeResponse
    {
        public string Name { get; set; }
        public string Mac { get; set; }
        public string Id { get; set; }
        public Network Network { get; set; }
        public Usage5 Usage { get; set; }
    }

    public class Usage5
    {
        public int Total { get; set; }
        public int Upstream { get; set; }
        public int Downstream { get; set; }
        public double Percentage { get; set; }
    }
}