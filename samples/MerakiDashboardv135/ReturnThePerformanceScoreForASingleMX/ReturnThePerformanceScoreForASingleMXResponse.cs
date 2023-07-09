using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<ReturnThePerformanceScoreForASingleMXResponse>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class ReturnThePerformanceScoreForASingleMXResponse
    {
        public int PerfScore { get; set; }
    }
}