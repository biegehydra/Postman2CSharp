using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<List<NAnOverviewOfAlertOccurrencesOverATimespanByMetricResponse>>(myJsonResponse);
    public class NAnOverviewOfAlertOccurrencesOverATimespanByMetricResponse
    {
        public DateTime StartTs { get; set; }
        public DateTime EndTs { get; set; }
        public Counts2 Counts { get; set; }
    }
}