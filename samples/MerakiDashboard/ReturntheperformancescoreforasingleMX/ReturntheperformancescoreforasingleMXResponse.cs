using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<ReturntheperformancescoreforasingleMXResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class ReturntheperformancescoreforasingleMXResponse
    {
        public int PerfScore { get; set; }
    }
}