using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<List<ListTheVLANsForAnMXNetworkResponse>>(myJsonResponse);
    public class ListTheVLANsForAnMXNetworkResponse
    {
        public string Id { get; set; }
        public string InterfaceId { get; set; }
        public string Name { get; set; }
        public string Subnet { get; set; }
        public string ApplianceIp { get; set; }
        public string GroupPolicyId { get; set; }
        public string TemplateVlanType { get; set; }
        public string Cidr { get; set; }
        public int Mask { get; set; }
        public List<string> DhcpRelayServerIps { get; set; }
        public string DhcpHandling { get; set; }
        public string DhcpLeaseTime { get; set; }
        public bool DhcpBootOptionsEnabled { get; set; }
        public string DhcpBootNextServer { get; set; }
        public string DhcpBootFilename { get; set; }
        public List<ReservedIpRanges> ReservedIpRanges { get; set; }
        public string DnsNameservers { get; set; }
        public List<DhcpOptions> DhcpOptions { get; set; }
        public string VpnNatSubnet { get; set; }
        public Ipv6 MandatoryDhcp { get; set; }
        public Ipv6 Ipv6 { get; set; }
    }

    public class DhcpOptions
    {
        public string Code { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
    }
}