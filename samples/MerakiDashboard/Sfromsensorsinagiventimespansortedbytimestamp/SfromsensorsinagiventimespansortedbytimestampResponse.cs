using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<SfromsensorsinagiventimespansortedbytimestampResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class SfromsensorsinagiventimespansortedbytimestampResponse
    {
        public string Serial { get; set; }
        public Network Network { get; set; }
        public DateTime Ts { get; set; }
        public string Metric { get; set; }
        public Battery Battery { get; set; }
        public Button Button { get; set; }
        public Door Door { get; set; }
        public Humidity7 Humidity { get; set; }
        public IndoorAirQuality7 IndoorAirQuality { get; set; }
        public Noise Noise { get; set; }
        public Pm25 Pm25 { get; set; }
        public Temperature7 Temperature { get; set; }
        public Tvoc7 Tvoc { get; set; }
        public Water Water { get; set; }
    }

    public class Network
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class Temperature7
    {
        public double Fahrenheit { get; set; }
        public double Celsius { get; set; }
    }

    public class Ambient7
    {
        public int Level { get; set; }
    }

    public class Battery
    {
        public int Percentage { get; set; }
    }

    public class Button
    {
        public string PressType { get; set; }
    }

    public class Humidity7
    {
        public int RelativePercentage { get; set; }
    }

    public class IndoorAirQuality7
    {
        public int Score { get; set; }
    }

    public class Pm257
    {
        public int Concentration { get; set; }
    }

    public class Tvoc7
    {
        public int Concentration { get; set; }
    }
}