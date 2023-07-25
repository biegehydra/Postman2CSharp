using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<List<GetTheSentAndReceivedBytesForEachUplinkOfANetworkResponse>>(myJsonResponse);
    public class GetTheSentAndReceivedBytesForEachUplinkOfANetworkResponse
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public List<ByInterface> ByInterface { get; set; }
    }

    public class ByInterface
    {
        public string Interface { get; set; }
        public int Sent { get; set; }
        public int Received { get; set; }
    }
}