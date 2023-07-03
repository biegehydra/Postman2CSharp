using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<CreatesASensorAlertProfileForANetworkRequest>(myJsonResponse);
namespace MerakiDashboard
{
    public class CreatesASensorAlertProfileForANetworkRequest
    {
        public string Name { get; set; }
        public List<Conditions> Conditions { get; set; }
        public Schedule2 Schedule { get; set; }
        public Recipients Recipients { get; set; }
        public List<string> Serials { get; set; }
    }

    public class Temperature2
    {
        public string Celsius { get; set; }
        public string Fahrenheit { get; set; }
        public string Quality { get; set; }
    }

    public class Ambient2
    {
        public string Level { get; set; }
        public string Quality { get; set; }
    }

    public class Humidity2
    {
        public string RelativePercentage { get; set; }
        public string Quality { get; set; }
    }

    public class IndoorAirQuality2
    {
        public string Score { get; set; }
        public string Quality { get; set; }
    }

    public class Pm252
    {
        public string Concentration { get; set; }
        public string Quality { get; set; }
    }

    public class Tvoc2
    {
        public string Concentration { get; set; }
        public string Quality { get; set; }
    }

    public class Door2
    {
        public string Open { get; set; }
    }

    public class Schedule2
    {
        public string Id { get; set; }
    }

    public class Water2
    {
        public string Present { get; set; }
    }
}