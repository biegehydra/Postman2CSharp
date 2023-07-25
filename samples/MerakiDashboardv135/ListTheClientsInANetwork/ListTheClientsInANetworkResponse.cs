using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<List<ListTheClientsInANetworkResponse>>(myJsonResponse);
    public class ListTheClientsInANetworkResponse
    {
        public Usage Usage { get; set; }
        public string Id { get; set; }
        public string Description { get; set; }
        public string Mac { get; set; }
        public string Ip { get; set; }
        public string User { get; set; }
        public int Vlan { get; set; }
        public object Switchport { get; set; }
        public string Ip6 { get; set; }
        public int FirstSeen { get; set; }
        public int LastSeen { get; set; }
        public string Manufacturer { get; set; }
        public string Os { get; set; }
        public string RecentDeviceSerial { get; set; }
        public string RecentDeviceName { get; set; }
        public string RecentDeviceMac { get; set; }
        public string Ssid { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
        public string Ip6Local { get; set; }
        public bool SmInstalled { get; set; }
        public string GroupPolicy8021x { get; set; }
    }

    public class Usage
    {
        public int Sent { get; set; }
        public int Recv { get; set; }
    }
}