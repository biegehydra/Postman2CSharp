using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<ListAirMarshalScanResultsFromANetworkResponse>>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class ListAirMarshalScanResultsFromANetworkResponse
    {
        public string Ssid { get; set; }
        public List<Bssids> Bssids { get; set; }
        public List<int> Channels { get; set; }
        public int FirstSeen { get; set; }
        public int LastSeen { get; set; }
        public List<string> WiredMacs { get; set; }
        public List<int> WiredVlans { get; set; }
        public int WiredLastSeen { get; set; }
    }

    public class Bssids
    {
        public string Bssid { get; set; }
        public bool Contained { get; set; }
        public List<DetectedBy> DetectedBy { get; set; }
    }

    public class DetectedBy
    {
        public string Device { get; set; }
        public int Rssi { get; set; }
    }
}