using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<GeChannelUtilizationForAllBandsInANetworkSplitByAPResponse>>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class GeChannelUtilizationForAllBandsInANetworkSplitByAPResponse
    {
        public string Serial { get; set; }
        public string Mac { get; set; }
        public Network Network { get; set; }
        public List<ByBand> ByBand { get; set; }
    }

    public class ByBand
    {
        public string Band { get; set; }
        public Wifi Wifi { get; set; }
        public NonWifi NonWifi { get; set; }
        public Total Total { get; set; }
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