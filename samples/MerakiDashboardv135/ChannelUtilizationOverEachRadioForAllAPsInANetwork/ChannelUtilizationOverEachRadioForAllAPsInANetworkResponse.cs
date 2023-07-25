using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<List<ChannelUtilizationOverEachRadioForAllAPsInANetworkResponse>>(myJsonResponse);
    public class ChannelUtilizationOverEachRadioForAllAPsInANetworkResponse
    {
        public string Serial { get; set; }
        public string Model { get; set; }
        public string Tags { get; set; }
        public List<Wifi0> Wifi0 { get; set; }
        public List<Wifi0> Wifi1 { get; set; }
    }

    public class Wifi0
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double UtilizationTotal { get; set; }
        public double Utilization80211 { get; set; }
        public double UtilizationNon80211 { get; set; }
    }
}