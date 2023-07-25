using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<UpdateAVLANRequest>(myJsonResponse);
    public class UpdateAVLANRequest
    {
        public string Name { get; set; }
        public string Subnet { get; set; }
        public string ApplianceIp { get; set; }
        public string GroupPolicyId { get; set; }
        public string VpnNatSubnet { get; set; }
        public string DhcpHandling { get; set; }
        public List<string> DhcpRelayServerIps { get; set; }
        public string DhcpLeaseTime { get; set; }
        public string DhcpBootOptionsEnabled { get; set; }
        public string DhcpBootNextServer { get; set; }
        public string DhcpBootFilename { get; set; }
        public Ipv7 FixedIpAssignments { get; set; }
        public List<ReservedIpRanges> ReservedIpRanges { get; set; }
        public string DnsNameservers { get; set; }
        public List<DhcpOptions> DhcpOptions { get; set; }
        public string TemplateVlanType { get; set; }
        public string Cidr { get; set; }
        public string Mask { get; set; }
        public Ipv7 Ipv6 { get; set; }
        public Ipv7 MandatoryDhcp { get; set; }
    }
}