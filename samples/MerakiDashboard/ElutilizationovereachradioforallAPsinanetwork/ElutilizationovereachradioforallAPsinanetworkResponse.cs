using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<ElutilizationovereachradioforallAPsinanetworkResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class ElutilizationovereachradioforallAPsinanetworkResponse
    {
        public string Serial { get; set; }
        public string Model { get; set; }
        public string Tags { get; set; }
        public List<Wifi0> Wifi0 { get; set; }
        public List<Wifi1> Wifi1 { get; set; }
    }

    public class Wifi0
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double UtilizationTotal { get; set; }
        public double Utilization80211 { get; set; }
        public double UtilizationNon80211 { get; set; }
    }

    public class Wifi1
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double UtilizationTotal { get; set; }
        public double Utilization80211 { get; set; }
        public double UtilizationNon80211 { get; set; }
    }
}