using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<List<ReturnTheClientsDailyCellularDataUsageHistoryResponse>>(myJsonResponse);
    public class ReturnTheClientsDailyCellularDataUsageHistoryResponse
    {
        public int Received { get; set; }
        public int Sent { get; set; }
        public DateTime Ts { get; set; }
    }
}