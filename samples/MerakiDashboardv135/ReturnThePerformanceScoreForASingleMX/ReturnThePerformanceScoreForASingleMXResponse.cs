using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<ReturnThePerformanceScoreForASingleMXResponse>(myJsonResponse);
    public class ReturnThePerformanceScoreForASingleMXResponse
    {
        public int PerfScore { get; set; }
    }
}