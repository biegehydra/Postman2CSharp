using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<ListsAllSensorAlertProfilesForANetworkResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class ListsAllSensorAlertProfilesForANetworkResponse
    {
        public string ProfileId { get; set; }
        public string Name { get; set; }
        public BlockedUrlCategories Schedule { get; set; }
        public List<Conditions> Conditions { get; set; }
        public Recipients Recipients { get; set; }
        public List<string> Serials { get; set; }
    }

    public class Threshold
    {
        public Temperature Temperature { get; set; }
        public Humidity Humidity { get; set; }
        public Water Water { get; set; }
        public Door Door { get; set; }
        public Tvoc Tvoc { get; set; }
        public Pm25 Pm25 { get; set; }
        public Noise Noise { get; set; }
        public IndoorAirQuality IndoorAirQuality { get; set; }
    }

    public class Conditions
    {
        public string Metric { get; set; }
        public Threshold Threshold { get; set; }
        public string Direction { get; set; }
        public int Duration { get; set; }
    }

    public class Recipients
    {
        public List<string> Emails { get; set; }
        public List<string> SmsNumbers { get; set; }
        public List<string> HttpServerIds { get; set; }
    }

    public class Temperature
    {
        public double Celsius { get; set; }
        public int Fahrenheit { get; set; }
        public string Quality { get; set; }
    }

    public class Ambient
    {
        public int Level { get; set; }
        public string Quality { get; set; }
    }

    public class Humidity
    {
        public int RelativePercentage { get; set; }
        public string Quality { get; set; }
    }

    public class IndoorAirQuality
    {
        public int Score { get; set; }
        public string Quality { get; set; }
    }

    public class Pm25
    {
        public int Concentration { get; set; }
        public string Quality { get; set; }
    }

    public class Tvoc
    {
        public int Concentration { get; set; }
        public string Quality { get; set; }
    }

    public class Door
    {
        public bool Open { get; set; }
    }

    public class Noise
    {
        public Ambient Ambient { get; set; }
    }

    public class Water
    {
        public bool Present { get; set; }
    }
}