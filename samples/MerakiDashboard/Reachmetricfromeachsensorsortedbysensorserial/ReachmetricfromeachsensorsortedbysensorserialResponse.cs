using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<REachMetricFromEachSensorSortedBySensorSerialResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class REachMetricFromEachSensorSortedBySensorSerialResponse
    {
        public string Serial { get; set; }
        public Network Network { get; set; }
        public List<Readings> Readings { get; set; }
    }

    public class Readings
    {
        public DateTime Ts { get; set; }
        public string Metric { get; set; }
        public Battery Battery { get; set; }
        public Button Button { get; set; }
        public Door Door { get; set; }
        public Humidity Humidity { get; set; }
        public IndoorAirQuality IndoorAirQuality { get; set; }
        public Noise Noise { get; set; }
        public Pm25 Pm25 { get; set; }
        public Temperature Temperature { get; set; }
        public Tvoc Tvoc { get; set; }
        public Water Water { get; set; }
    }
}