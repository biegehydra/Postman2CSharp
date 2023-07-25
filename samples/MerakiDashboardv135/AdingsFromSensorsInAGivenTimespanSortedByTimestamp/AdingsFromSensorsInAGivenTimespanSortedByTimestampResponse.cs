using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<List<AdingsFromSensorsInAGivenTimespanSortedByTimestampResponse>>(myJsonResponse);
    public class AdingsFromSensorsInAGivenTimespanSortedByTimestampResponse
    {
        public string Serial { get; set; }
        public ListTheOrganizationsResponse Network { get; set; }
        public DateTime Ts { get; set; }
        public string Metric { get; set; }
        public Battery Battery { get; set; }
        public Button Button { get; set; }
        public Door Door { get; set; }
        public Humidity Humidity { get; set; }
        public IndoorAirQuality IndoorAirQuality { get; set; }
        public Noise Noise { get; set; }
        public Pm25 Pm25 { get; set; }
        public Temperature2 Temperature { get; set; }
        public Pm25 Tvoc { get; set; }
        public Water Water { get; set; }
    }

    public class Temperature2
    {
        public double Fahrenheit { get; set; }
        public double Celsius { get; set; }
    }

    public class Battery
    {
        public int Percentage { get; set; }
    }

    public class Button
    {
        public string PressType { get; set; }
    }
}