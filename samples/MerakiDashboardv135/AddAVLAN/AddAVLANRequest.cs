using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MerakiDashboard
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<AddAVLANRequest>(myJsonResponse);
    public class AddAVLANRequest
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Subnet { get; set; }
        public string ApplianceIp { get; set; }
        public string GroupPolicyId { get; set; }
        public string TemplateVlanType { get; set; }
        public string Cidr { get; set; }
        public string Mask { get; set; }
        public Ipv7 Ipv6 { get; set; }
        public Ipv6 MandatoryDhcp { get; set; }
    }

    public class PrefixAssignments2
    {
        public string Autonomous { get; set; }
        public string StaticPrefix { get; set; }
        public string StaticApplianceIp6 { get; set; }
        public Origin2 Origin { get; set; }
    }

    public class Ipv7
    {
        public string Enabled { get; set; }
        public List<PrefixAssignments2> PrefixAssignments { get; set; }
    }
}