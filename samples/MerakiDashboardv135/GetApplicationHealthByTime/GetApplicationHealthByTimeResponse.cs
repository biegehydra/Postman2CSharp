using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<List<GetApplicationHealthByTimeResponse>>(myJsonResponse);
    public class GetApplicationHealthByTimeResponse
    {
        public DateTime StartTs { get; set; }
        public DateTime EndTs { get; set; }
        public int WanGoodput { get; set; }
        public int LanGoodput { get; set; }
        public double WanLatencyMs { get; set; }
        public double LanLatencyMs { get; set; }
        public double WanLossPercent { get; set; }
        public int LanLossPercent { get; set; }
        public int ResponseDuration { get; set; }
        public int Sent { get; set; }
        public int Recv { get; set; }
        public int NumClients { get; set; }
    }
}