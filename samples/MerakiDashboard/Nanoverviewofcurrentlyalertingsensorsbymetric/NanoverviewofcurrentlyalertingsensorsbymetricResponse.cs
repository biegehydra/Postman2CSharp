using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<NanoverviewofcurrentlyalertingsensorsbymetricResponse>(myJsonResponse);
namespace MerakiDashboard
{
    public class NanoverviewofcurrentlyalertingsensorsbymetricResponse
    {
        public List<string> SupportedMetrics { get; set; }
        public Counts2 Counts { get; set; }
    }

    public class Counts2
    {
        public int Door { get; set; }
        public int Humidity { get; set; }
        public int IndoorAirQuality { get; set; }
        public Noise7 Noise { get; set; }
        public int Pm25 { get; set; }
        public int Temperature { get; set; }
        public int Tvoc { get; set; }
        public int Water { get; set; }
    }

    public class Noise7
    {
        public int Ambient { get; set; }
    }
}