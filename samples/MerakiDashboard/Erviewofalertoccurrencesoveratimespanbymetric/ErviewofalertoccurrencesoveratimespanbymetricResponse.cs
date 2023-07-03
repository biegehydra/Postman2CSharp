using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<ErviewOfAlertOccurrencesOverATimespanByMetricResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class ErviewOfAlertOccurrencesOverATimespanByMetricResponse
    {
        public DateTime StartTs { get; set; }
        public DateTime EndTs { get; set; }
        public Counts Counts { get; set; }
    }
}