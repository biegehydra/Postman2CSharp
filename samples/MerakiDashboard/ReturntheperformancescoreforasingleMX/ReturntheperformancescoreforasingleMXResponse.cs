using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<ReturnThePerformanceScoreForASingleMXResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class ReturnThePerformanceScoreForASingleMXResponse
    {
        public int PerfScore { get; set; }
    }
}