using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<ListTheClientsThatHaveUsedThisNetworkInTheTimespanResponse>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class ListTheClientsThatHaveUsedThisNetworkInTheTimespanResponse
    {
        public string Id { get; set; }
        public string Mac { get; set; }
        public string Ip { get; set; }
        public string Ip6 { get; set; }
        public string Description { get; set; }
        public int FirstSeen { get; set; }
        public int LastSeen { get; set; }
        public string Manufacturer { get; set; }
        public string Os { get; set; }
        public string User { get; set; }
        public string Vlan { get; set; }
        public string Ssid { get; set; }
        public string Switchport { get; set; }
        public string WirelessCapabilities { get; set; }
        public bool SmInstalled { get; set; }
        public string RecentDeviceMac { get; set; }
        public string Status { get; set; }
        public Usage Usage { get; set; }
        public string NamedVlan { get; set; }
        public string AdaptivePolicyGroup { get; set; }
        public string DeviceTypePrediction { get; set; }
        public string RecentDeviceSerial { get; set; }
        public string RecentDeviceName { get; set; }
        public string RecentDeviceConnection { get; set; }
        public string Notes { get; set; }
        public string Ip6Local { get; set; }
        public string GroupPolicy8021x { get; set; }
        public string PskGroup { get; set; }
    }
}