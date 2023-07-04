using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<NnelUtilizationForAllBandsInANetworkSplitByAPResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class NnelUtilizationForAllBandsInANetworkSplitByAPResponse
    {
        public string Serial { get; set; }
        public string Mac { get; set; }
        public Profile3 Network { get; set; }
        public List<ByBand> ByBand { get; set; }
    }

    public class ByBand
    {
        public string Band { get; set; }
        public Wifi Wifi { get; set; }
        public NonWifi NonWifi { get; set; }
        public Battery Total { get; set; }
    }

    public class NonWifi
    {
        public double Percentage { get; set; }
    }

    public class Wifi
    {
        public double Percentage { get; set; }
    }
}