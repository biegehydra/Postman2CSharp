using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<ReturnAnOverviewOfCurrentlyAlertingSensorsByMetricResponse>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class ReturnAnOverviewOfCurrentlyAlertingSensorsByMetricResponse
    {
        public List<string> SupportedMetrics { get; set; }
        public Counts2 Counts { get; set; }
    }

    public class Counts2
    {
        public int Door { get; set; }
        public int Humidity { get; set; }
        public int IndoorAirQuality { get; set; }
        public Noise2 Noise { get; set; }
        public int Pm25 { get; set; }
        public int Temperature { get; set; }
        public int Tvoc { get; set; }
        public int Water { get; set; }
    }

    public class Noise2
    {
        public int Ambient { get; set; }
    }
}