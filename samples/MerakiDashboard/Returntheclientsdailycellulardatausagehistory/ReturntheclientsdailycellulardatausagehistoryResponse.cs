using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<ReturnTheClientsDailyCellularDataUsageHistoryResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class ReturnTheClientsDailyCellularDataUsageHistoryResponse
    {
        public int Received { get; set; }
        public int Sent { get; set; }
        public DateTime Ts { get; set; }
    }
}