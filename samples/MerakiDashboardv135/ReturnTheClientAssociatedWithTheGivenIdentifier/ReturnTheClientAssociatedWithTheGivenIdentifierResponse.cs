using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<ReturnTheClientAssociatedWithTheGivenIdentifierResponse>(myJsonResponse);
namespace MerakiDashboardv135
{
    public class ReturnTheClientAssociatedWithTheGivenIdentifierResponse
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
        public List<ClientVpnConnections> ClientVpnConnections { get; set; }
        public List<List<string>> Lldp { get; set; }
        public List<List<string>> Cdp { get; set; }
        public string Status { get; set; }
    }

    public class ClientVpnConnections
    {
        public string RemoteIp { get; set; }
        public int ConnectedAt { get; set; }
        public int DisconnectedAt { get; set; }
    }
}