using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<NnelutilizationforallbandsinanetworksplitbyAPResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class NnelutilizationforallbandsinanetworksplitbyAPResponse
    {
        public string Serial { get; set; }
        public string Mac { get; set; }
        public Network3 Network { get; set; }
        public List<ByBand> ByBand { get; set; }
    }

    public class ByBand
    {
        public string Band { get; set; }
        public Wifi Wifi { get; set; }
        public NonWifi NonWifi { get; set; }
        public Total Total { get; set; }
    }

    public class Network3
    {
        public string Id { get; set; }
    }

    public class NonWifi
    {
        public double Percentage { get; set; }
    }

    public class Total
    {
        public int Percentage { get; set; }
    }

    public class Wifi
    {
        public double Percentage { get; set; }
    }
}