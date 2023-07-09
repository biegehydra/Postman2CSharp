using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<NAnOverviewOfAlertOccurrencesOverATimespanByMetricResponse>>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class NAnOverviewOfAlertOccurrencesOverATimespanByMetricResponse
    {
        public DateTime StartTs { get; set; }
        public DateTime EndTs { get; set; }
        public Counts2 Counts { get; set; }
    }
}