using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// Root myDeserializedClass = JsonSerializer.Deserialize<List<ShowVPNstatusfornetworksinanorganizationResponse>>(myJsonResponse);
namespace MerakiDashboard
{
    public class ShowVPNstatusfornetworksinanorganizationResponse
    {
        public string NetworkId { get; set; }
        public string NetworkName { get; set; }
        public string DeviceSerial { get; set; }
        public string DeviceStatus { get; set; }
        public List<Uplinks2> Uplinks { get; set; }
        public string VpnMode { get; set; }
        public List<ExportedSubnets> ExportedSubnets { get; set; }
        public List<MerakiVpnPeers2> MerakiVpnPeers { get; set; }
        public List<ThirdPartyVpnPeers> ThirdPartyVpnPeers { get; set; }
    }

    public class MerakiVpnPeers2
    {
        public string NetworkId { get; set; }
        public string NetworkName { get; set; }
        public string Reachability { get; set; }
    }

    public class ThirdPartyVpnPeers
    {
        public string Name { get; set; }
        public string PublicIp { get; set; }
        public string Reachability { get; set; }
    }

    public class ExportedSubnets
    {
        public string Subnet { get; set; }
        public string Name { get; set; }
    }

    public class Uplinks2
    {
        public string Interface { get; set; }
        public string PublicIp { get; set; }
    }
}